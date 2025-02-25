﻿using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using TagsTree.Services;
using static TagsTree.Properties.Settings;

namespace TagsTree.Views
{
	/// <summary>
	/// NewConfig.xaml 的交互逻辑
	/// </summary>
	public partial class NewConfig : Window
	{
		public NewConfig(Window owner)
		{
			Owner = owner;
			InitializeComponent();
			MouseLeftButtonDown += (_, _) => DragMove();
			TbConfigPath.Text = Default.ConfigPath;
			TbLibraryPath.Text = Default.LibraryPath;
			CbRootFoldersExist.IsChecked = Default.RootFoldersExist;
		}

		private void BConfigPath_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog("选择存放配置文件的文件夹") { IsFolderPicker = true };
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				TbConfigPath.Text = dialog.FileName;
		}

		private void BLibraryPath_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog("选择被归类文件的根目录文件夹") { IsFolderPicker = true };
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				TbLibraryPath.Text = dialog.FileName;
		}

		private void BConfirm_Click(object sender, RoutedEventArgs e)
		{
			var legalPath = new Regex(@"^[a-zA-Z]:\\[^\/\:\*\?\""\<\>\|]+$");
			if (!legalPath.IsMatch(TbConfigPath.Text) || !legalPath.IsMatch(TbLibraryPath.Text))
				App.ErrorMessageBox("路径错误！请填写正确完整的文件夹路径！");
			else if (App.LoadConfig(TbConfigPath.Text) == true)
			{
				Default.ConfigPath = TbConfigPath.Text;
				Default.LibraryPath = TbLibraryPath.Text;
				Default.RootFoldersExist = CbRootFoldersExist.IsChecked!.Value;
				Default.IsSet = true;
				Default.Save();
				DialogResult = true;
				Close();
			}
		}
	}
}