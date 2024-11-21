﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Form1 : Form
    {
        private BindingSource showProductList = new BindingSource();
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] categories = { "Beverages", "Bread", "Cereals", "Dairy", "Snacks and Sweets", "Meat", "Other" };
            foreach (string category in categories)
            {
                cbCategory.Items.Add(category);
            }
        }
       
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = ValidateStringInput(tbProductName.Text, "Product Name");
                _Category = cbCategory.Text;
                _MfgDate = dtpMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtpExpDate.Value.ToString("yyyy-MM-dd");
                _Description = rtbDescription.Text;
                _Quantity = ValidateIntegerInput(tbQuantity.Text, "Quantity");
                _SellPrice = ValidateCurrencyInput(tbSellPrice.Text, "Selling Price");

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                dgvProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvProductList.DataSource = showProductList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ValidateStringInput(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new StringFormatException($"{fieldName} cannot be empty.");
            return input;
        }

        private int ValidateIntegerInput(string input, string fieldName)
        {
            if (!int.TryParse(input, out int result) || result < 0)
                throw new NumberFormatException($"{fieldName} must be a positive integer.");
            return result;
        }

        private double ValidateCurrencyInput(string input, string fieldName)
        {
            if (!double.TryParse(input, out double result) || result < 0)
                throw new CurrencyFormatException($"{fieldName} must be a valid positive number.");
            return result;
        }
    }

    public class ProductClass
        {
            private int _Quantity;
            private double _SellingPrice;
            private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

            public ProductClass(string ProductName, string Category, string MfgDate, string ExpDate,
                                double Price, int Quantity, string Description)
            {
                this._Quantity = Quantity;
                this._SellingPrice = Price;
                this._ProductName = ProductName;
                this._Category = Category;
                this._ManufacturingDate = MfgDate;
                this._ExpirationDate = ExpDate;
                this._Description = Description;
            }

            public string ProductName { get => _ProductName; set => _ProductName = value; }
            public string Category { get => _Category; set => _Category = value; }
            public string ManufacturingDate { get => _ManufacturingDate; set => _ManufacturingDate = value; }
            public string ExpirationDate { get => _ExpirationDate; set => _ExpirationDate = value; }
            public string Description { get => _Description; set => _Description = value; }
            public int Quantity { get => _Quantity; set => _Quantity = value; }
            public double SellingPrice { get => _SellingPrice; set => _SellingPrice = value; }
        }

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }
}


