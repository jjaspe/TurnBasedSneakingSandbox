using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SneakingCreationWithForms;
using SneakingCreationWithForms.MVP;
using Sneaking_Gameplay.Sneaking_Drawables;

namespace SneakingCreationWithForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Presenter newPresenter = new Presenter();
            IModel model = new ExampleModel();
            model.Map = SneakingMap.createInstance(0, 0, 0, null);
            newPresenter.Model = model;
            IView view = new MainForm();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
