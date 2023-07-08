using System;
using System.Diagnostics;
using System.IO;

class Program
{
    private static string seewoFolder;

    static void Main()
    {
        Console.WriteLine("第六代希沃桌面适配软件 v1.0\nGithub:https://github.com/NickPikachu/\n请确保您的第五代希沃教育一体机处于原版系统状态并安装第六代希沃管家和希沃助手后使用本程序！\n请输入“enter”继续使用本程序！");
        Console.ReadLine();

        string rootFolder = @"C:\ProgramData\LightAppRendersResources\";

        // 获取指定目录下以 "seewo-lightapp-launcher" 开头的文件夹
        string[] subfolders = Directory.GetDirectories(rootFolder, "seewo-lightapp-launcher*");

        if (subfolders.Length == 1)
        {
            seewoFolder = subfolders[0];
            Console.WriteLine("找到以 \"seewo-lightapp-launcher\" 开头的文件夹：" + seewoFolder);

            string mainJsPath = Path.Combine(seewoFolder, "main.js");
            if (File.Exists(mainJsPath))
            {
                // 备份 main.js 文件
                string backupFilePath = Path.Combine(seewoFolder, "main_backup.js");
                File.Copy(mainJsPath, backupFilePath, true);
                Console.WriteLine("成功备份 main.js 为 main_backup.js");

                // 读取 main.js 文件的所有行
                string[] lines = File.ReadAllLines(mainJsPath);

                if (lines.Length >= 129)
                {
                    // 替换第129行中的 "false" 为 "true"
                    lines[128] = lines[128].Replace("false", "true");

                    // 覆盖保存更新后的 main.js 文件
                    File.WriteAllLines(mainJsPath, lines);

                    Console.WriteLine("成功替换词 \"false\" 为 \"true\".");
                }
                else
                {
                    Console.WriteLine("main.js 文件行数不足129行.");
                }
            }
            else
            {
                Console.WriteLine("未找到 main.js 文件.");
            }
        }
        else
        {
            Console.WriteLine("未找到或找到多个以 \"seewo-lightapp-launcher\" 开头的文件夹.");
        }

        string variableName = "MAUMainVersion";
        string variableValue = "6";

        string arguments = $"{variableName} {variableValue} /M";
        Process.Start("setx", arguments);

        // 通知用户已经修改了系统环境变量
        Console.WriteLine($"已经修改了系统环境变量：{variableName}，值为：{variableValue}");

        Console.ReadLine();
    }
}
