﻿using ELIMPERIOBARYCAFE.MVVM.View;

namespace ELIMPERIOBARYCAFE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NewPage1());
        }
    }
}
