using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Shapes;
using System.Linq;

namespace Recolor
{
    public partial class MainWindow : Window
    {
        string[] allCheckBoxItems = new string[] { "diamond_sword.png", "diamond_hoe.png", "diamond_axe.png", "diamond_chestplate.png"
            , "diamond_leggings.png", "diamond_boots.png", "diamond_helmet.png", "diamond_pickaxe.png", "diamond_shovel.png", "fishing_rod_cast.png", "fishing_rod_uncast.png"
        ,"ender_pearl.png", "diamond_ore.png", "diamond_block.png","diamond_layer_1.png","diamond_layer_2.png","awd"};

        Config config = new Config();
        List<Item> selectedItems = new List<Item>();
        
        public MainWindow()
        {
            InitializeComponent();
            createCheckBoxes();

        }
        public void createCheckBoxes()
        {
            for (int i = 0; i < allCheckBoxItems.Length; i++)
            {
                var checkY = new CheckBox
                {
                    
                    Content =  allCheckBoxItems[i],
                    IsChecked = true

                };
                checkY.Checked += ChkClazz_OnCheckChanged;
                checkY.Unchecked += ChkClazz_OnCheckChanged;
                lst_View_ItemsSelect.Items.Add(checkY);
                config.whichCheckboxesOn.Add(allCheckBoxItems[i]);
            }

        }
        private void checkCheckBoxes()
        {
            lst_View_ItemsSelect.Items.Clear();
            foreach(var itemName in selectedItems)
            {
                if(File.Exists(itemName.savepath))
                {
                    var checkY = new CheckBox
                    {

                        Content = itemName.name,
                        IsChecked = true

                    };
                    checkY.Checked += ChkClazz_OnCheckChanged;
                    checkY.Unchecked += ChkClazz_OnCheckChanged;
                    lst_View_ItemsSelect.Items.Add(checkY);
                }
               
            }

        }
        private void ChkClazz_OnCheckChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string? subject = checkBox.Content as string;
                if (subject == null) return;
                if (checkBox.IsChecked == true)
                {
                    if (!config.whichCheckboxesOn.Contains(subject))
                    {
                        config.whichCheckboxesOn.Add(subject);
                        editSelectedItems(subject, true);
                    }
                }
                else
                {
                    if (config.whichCheckboxesOn.Contains(subject))
                    {
                        config.whichCheckboxesOn.Remove(subject);
                        editSelectedItems(subject, false);
                    }
                }
            }
           // itemEdit();
            itemLoad();


        }
        private void editSelectedItems(String itemName, bool isRecolor)
        {
            List<Item> copyList = selectedItems.ToList();
           foreach(var item in copyList)
            {
                if(item.name == itemName)
                {
                    if (isRecolor)
                    {
                        selectedItems.Add(new Item { name = item.name, itempath = item.itempath, savepath = item.savepath, isToRecolor = true });
                        selectedItems.Remove(item);
                    }
                    else
                    {
                        selectedItems.Add(new Item { name = item.name, itempath = item.itempath, savepath = item.savepath, isToRecolor = false });
                        selectedItems.Remove(item);
                    }
                }
            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            colorToShow.Background = new SolidColorBrush(Color.FromRgb((byte)sl_red.Value, (byte)sl_green.Value, (byte)sl_blue.Value));
            config.red = (int)sl_red.Value;
            config.green = (int)sl_green.Value;
            config.blue = (int)sl_blue.Value;
        }

        private void Button_Click_OpenFolder(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ZIP-Dateien (*.zip)|*.zip";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedZipFilePath = openFileDialog.FileName;
                string[] pathParts = selectedZipFilePath.Split('\\');
                string path = "";
                for (int i = 0; i < pathParts.Length - 1; i++)
                {
                    path += pathParts[i] + "\\";
                }
                int random = new System.Random().Next(0, 1000);

                config.pathToRecolor = path +  random + "_PackToRecolor";
                config.pathToSave = path + "save\\" + random + "_PackRecolored";
                ZipFile.ExtractToDirectory(selectedZipFilePath, config.pathToRecolor);
                ZipFile.ExtractToDirectory(selectedZipFilePath, config.pathToSave);
                loadAllItems();
                checkCheckBoxes();
                //itemEdit();
                itemLoad();
                foreach (Item item in selectedItems)
                {
                    if (File.Exists(item.itempath))
                    {
                        File.Delete(item.itempath);
                    }
                }
            }
            
        }
        private void loadAllItems()
        {
            foreach (var item in config.whichCheckboxesOn)
            {
                string pathLast = getPathOfItem(item);
                string path = config.pathToRecolor + pathLast;
                string savePath = config.pathToSave + pathLast;
                if (allCheckBoxItems.Contains(item))
                {
                
                       // allItems.Add(new Item { name = item, itempath = path, savepath = savePath });
                        selectedItems.Add(new Item { name = item, itempath = path, savepath = savePath, isToRecolor=true});
                }
            }
        }

        private void IsBrownColorFilter(object sender, RoutedEventArgs e)
        {
            if (ch_browncolorFilter.IsChecked == true)
            {
                config.isBrownColorFilterOn = true;
            }
            else
            {
                config.isBrownColorFilterOn = false;
            }
        }

        private void Button_Click_Recolor(object sender, RoutedEventArgs e)
        {
            string recoloredFolder = config.pathToRecolor;
            Directory.CreateDirectory(recoloredFolder);

            RecolorMake recolor = new RecolorMake();

            foreach (var item in selectedItems)
            {
                if (File.Exists(item.savepath))
                {
                    BitmapImage recoloredImage = null;

                    if(item.isToRecolor)
                    {
                        recoloredImage = recolor.RecolorItem(item.savepath, config.red, config.green, config.blue, config.isBrownColorFilterOn);
                    }
                    else
                    {
                        recoloredImage = new BitmapImage(new Uri(item.savepath));
                    }
                   

                    if (recoloredImage != null)
                    {
                        MessageBox.Show(recoloredFolder + getPathOfItem(item.name));
                        SaveBitmapImageAsPng(recoloredImage, recoloredFolder + getPathOfItem(item.name)); 
                    }
                }
            }
            selectedItems.Clear();
            lv_editedItems.Items.Clear();
            lv_defaultItems.Items.Clear();
            selectedItems.Clear();
            MessageBox.Show("Items recolored and saved to: " + recoloredFolder);
        }
        private void SaveBitmapImageAsPng(BitmapImage image, string filePath)
        {

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(stream);
            }

        }
        private string getPathOfItem(string item)
        {
            
            string toTextures = "\\assets\\minecraft\\textures";
            string toItems = "\\items\\";
            string toBlocks = "\\blocks\\";
            string toModels = "\\models\\armor\\";
            string toUse = "";

            if (item.Contains("ore.png") || item.Contains("block"))
            {
                toUse = toBlocks;
            }
            else if (item.Contains("layer"))
            {
                toUse = toModels;
            }
            else
            {
                toUse = toItems;
            }
            return toTextures + toUse + item;
        }
        private void itemEdit()
        {
            selectedItems.Clear();
            foreach( var item in allCheckBoxItems )
            {
                if (config.whichCheckboxesOn.Contains(item))
                {
                    foreach(var allItems in selectedItems)
                    {
                        if(allItems.name == item)
                        {
                            selectedItems.Add(allItems); break;
                        }
                    }
                }
            }
           
        }
        private void itemLoad()
        {

          lv_defaultItems.Items.Clear();
            foreach (var item in selectedItems)
            {
                if (File.Exists(item.savepath))
                {
                    if(item.isToRecolor)
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(item.savepath, UriKind.Relative);
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.EndInit();
                        var imageToAdd = new Image
                        {
                            Source = image,
                            Width = 60,
                            Height = 60

                        };
                        lv_defaultItems.Items.Add(imageToAdd);
                    }
                }

            }
        }
        private void itemRecolorLoad()
        {
            lv_editedItems.Items.Clear();
            foreach (var item in selectedItems)
            {
                if (File.Exists(item.savepath))
                {
                    if(item.isToRecolor)
                    {
                        RecolorMake recolor = new RecolorMake();
                        var image = recolor.RecolorItem(item.savepath, config.red, config.green, config.blue, config.isBrownColorFilterOn);
                        if (image == null) break;
                        var imageToAdd = new Image
                        {
                            Source = image,
                            Width = 60,
                            Height = 60

                        };

                        lv_editedItems.Items.Add(imageToAdd);
                    }
                }
            }
        }

        private void Button_Click_RecolorExample(object sender, RoutedEventArgs e)
        {
            itemRecolorLoad();
        }
    }

}