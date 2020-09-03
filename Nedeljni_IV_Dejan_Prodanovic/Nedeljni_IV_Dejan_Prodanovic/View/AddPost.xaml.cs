﻿using Nedeljni_IV_Dejan_Prodanovic.Model;
using Nedeljni_IV_Dejan_Prodanovic.ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nedeljni_IV_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for AddPost.xaml
    /// </summary>
    public partial class AddPost : Window
    {
        public AddPost()
        {
            InitializeComponent();
        }

        public AddPost(tblUser user)
        {
            InitializeComponent();
            DataContext = new AddPostViewModel(this, user);
        }
    }
}
