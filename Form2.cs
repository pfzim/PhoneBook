using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhoneBase
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			button1.DialogResult = DialogResult.OK;
		}
	}
}
