﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PriceCompareLogic;

namespace PriceCompareUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("he-IL");
            //This is ok for now, but do consider that a true MVVM application has no code behind in *.xaml.cs files
            //This can be accomplished via an MVVM framework- there are several of those with different code flavors.
            //One that is vastly used in CodeValue is Caliburn.Micro: http://caliburnmicro.com/
            DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
