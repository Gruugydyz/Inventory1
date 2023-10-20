using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory1
{
    public partial class frmAddProduct : Form

    {

        public frmAddProduct()
        {
            InitializeComponent();
            ProductBS = new BindingSource();
            showProductList = new BindingList<ProductClass>();
            ProductBS.DataSource = showProductList;
            gridViewProductList.DataSource = ProductBS;
            gridViewProductList.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;


        }

        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        private BindingSource ProductBS;
        private BindingList<ProductClass> showProductList;


        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;

        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages","Bread/Bakery", "Canned/Jarred Goods","Dairy","Frozen Goods","Meat",
                "Personal Care", "Other"

            };

            foreach (string variableName in ListOfProductCategory)
            {
                cbCategory.Items.Add(variableName);

            }
        }

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException("Product name.");
                }
                return name;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message);
                return name;
            }
            
        }
        public int Quantity(string qnty)
        {
            try
            {
                if (Regex.IsMatch(qnty, @"^[0-9]+$"))
                {
                    _Quantity = Convert.ToInt32(qnty);    
                    return _Quantity;
                }
                else
                {
                    throw new NumberFormatException("Quantity.");
                }
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show("Do you want to clear it?", "Warning",MessageBoxButtons.YesNo);           
            if (choice == DialogResult.Yes)
            {
               gridViewProductList.Rows.RemoveAt(0);
    
            }
        }

        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException("Selling price.");
                }
                return Convert.ToDouble(price);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show( ex.Message);
                return 0.0;
            }


        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string _ProductName) : base($"Invalid string format Occur for {_ProductName}") { }
        }
        public class NumberFormatException : Exception
        {
            public NumberFormatException(string _Quantity) : base($"Invalid number format Occur for {_Quantity}") { }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string _SellPrice) : base($"Invalid currency format Occur for {_SellPrice}") { }
        }

    }
}
