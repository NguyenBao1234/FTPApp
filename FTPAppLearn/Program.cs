namespace FTPAppLearn;

static class Program
{
    public static Random randomInst;
    [STAThread]
    static void Main()
    {
        randomInst = new Random();
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Main());
    }
}