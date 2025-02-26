using System.Windows;

namespace ToFLaC.View;

public partial class AboutProgramWindow : Window
{
    public AboutProgramWindow()
    {
        InitializeComponent();
        Text.Text =
            "Лабораторная работа по дисциплине 'Теория формальных языков и компиляторов'\n" +
            "Выполнили:\n" +
            "Студенты:\n" +
            "Хусаинов Артур Мансурович\n" +
            "Гордиенко Дмитрий Андреевич\n" +
            "Савочка Денис Владимирович\n" +
            "Группа: АВТ-213\n";
    }
}