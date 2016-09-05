using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;

namespace PhoneBase
{
    public partial class Form1 : Form
    {
		public static string datafile = null;
		public static bool unsaved = false;
		public static string config = null;
		public static System.Collections.ArrayList database = new System.Collections.ArrayList();
		public static string[] elements = { "name", "phone1", "phone2", "phone3", "mail", "organisation", "departament", "section", "position" };
		public Form1()
        {
            InitializeComponent();

			config = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\phonebook.cfg";
			if(File.Exists(config))
			{
				FileStream stream = new FileStream(config, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				System.Xml.XmlReader reader = new System.Xml.XmlTextReader(stream);

				bool getval = false;

				while(reader.Read())
				{
					switch(reader.NodeType)
					{
						case XmlNodeType.Element:
							if(reader.Name.CompareTo("basefile") == 0)
							{
								getval = true;
							}
							break;
						case XmlNodeType.Text:
							if(getval)
							{
								datafile = reader.Value;
								getval = false;
							}
							break;
					}
				}
				//datafile = "\\\\Fileserver\\Папка обмена\\Телефонный справочник\\base.xml";
				reader.Close();
				stream.Close();
			}

			LoadBase();

			//System.Security.SecurityManager.PolicyHierarchy().MoveNext();
			//textBox2.Text = System.Security.SecurityManager.PolicyHierarchy().Current.ToString();
        }

        private void UpdateList(string find)
        {
			ListViewItem lvItem;
			int i;

			string query = "";

			listView1.SelectedItems.Clear();
			listView1.Items.Clear();

			if ((comboBox1.SelectedIndex > 0) && (comboBox1.SelectedItem.ToString().Length > 0))
			{
				query = comboBox1.SelectedItem.ToString();
			}

			if ((comboBox2.SelectedIndex > 0) && (comboBox2.SelectedItem.ToString().Length > 0))
			{
				query += " " + comboBox2.SelectedItem.ToString();
			}

			if ((comboBox3.SelectedIndex > 0) && (comboBox3.SelectedItem.ToString().Length > 0))
			{
				query += " " + comboBox3.SelectedItem.ToString();
			}

			if ((find != null) && (find.Length > 0))
			{
				query += " " + find;
			}

			if((query != null) && (query.Length > 0))
            {
				textBox2.Text = "Последний запрос: " + query;
				string[] find_keys = query.ToLower().Split(' ');
				foreach(string[] row in database)
                {
					foreach(string find_key in find_keys)
					{
						if(find_key.Length > 0)
						{
							foreach(string cell in row)
							{
								if((cell != null) && cell.ToLower().Contains(find_key))
								{
									goto lb_next_key;
								}
							}
							goto lb_next_row;
						}
					lb_next_key: ;
					}

					lvItem = listView1.Items.Add(row[0]);
					lvItem.Tag = row;
					i = 1;
					while ((i < row.Length) && (i < listView1.Columns.Count))
					{
						lvItem.SubItems.Add(row[i]);
						i++;
					}
				lb_next_row: ;
                }
            }
            else
            {
				textBox2.Text = "Последний запрос: все контакты";
				foreach (string[] row in database)
                {
                    lvItem = listView1.Items.Add(row[0]);
					lvItem.Tag = row;
                    lvItem.SubItems.Add(row[1]);
                    lvItem.SubItems.Add(row[2]);
                    lvItem.SubItems.Add(row[3]);
                    lvItem.SubItems.Add(row[4]);
                    lvItem.SubItems.Add(row[5]);
                }
            }

			if(listView1.Items.Count > 0)
			{
				listView1.SelectedIndices.Add(0);
			}

			label2.Text = "Количество записей: " + listView1.Items.Count.ToString() + " (" + database.Count.ToString() + ")";
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//if((e.CloseReason == CloseReason.UserClosing) && (MessageBox.Show("Exit Phone book?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
			if(e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Close();
			Application.Exit();
		}

		private void notifyIcon1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStrip1.Show(MousePosition);
			}
			else
			{
				if(Visible)
				{
					Hide();
				}
				else
				{
					Show();
					if (WindowState == FormWindowState.Minimized)
					{
						WindowState = FormWindowState.Normal;
					}
					Activate();
				}
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if(WindowState == FormWindowState.Minimized)
			{
				Hide();
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				label12.Text = ((string[])listView1.SelectedItems[0].Tag)[0];
				label13.Text = ((string[])listView1.SelectedItems[0].Tag)[1];
				label14.Text = ((string[])listView1.SelectedItems[0].Tag)[2];
				label15.Text = ((string[])listView1.SelectedItems[0].Tag)[3];
				linkLabel1.Text = ((string[])listView1.SelectedItems[0].Tag)[4];
				linkLabel2.Text = ((string[])listView1.SelectedItems[0].Tag)[5];
				linkLabel3.Text = ((string[])listView1.SelectedItems[0].Tag)[6];
				linkLabel8.Text = ((string[])listView1.SelectedItems[0].Tag)[7];
				linkLabel4.Text = ((string[])listView1.SelectedItems[0].Tag)[8];
				//pictureBox1.Load("fotos\\logo.gif");
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:"+linkLabel1.Text);
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int i;
			i = 0;

			if(linkLabel2.Text.Length > 0)
			{
				i = comboBox1.FindString(linkLabel2.Text);
				if(i < 0)
				{
					i = 0;
				}
			}
			comboBox1.SelectedIndex = i;
			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;
		}

		private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int i;
			string dep = linkLabel3.Text;

			i = 0;

			if(linkLabel2.Text.Length > 0)
			{
				i = comboBox1.FindString(linkLabel2.Text);
				if(i < 0)
				{
					i = 0;
				}
			}
			comboBox1.SelectedIndex = i;

			if(linkLabel3.Text.Length > 0)
			{
				i = comboBox2.FindString(dep);
				if(i > 0)
				{
					comboBox2.SelectedIndex = i;
				}
			}
			comboBox3.SelectedIndex = 0;
		}

		private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int i;
			string dep = linkLabel3.Text;
			string sect = linkLabel8.Text;

			i = 0;

			if(linkLabel2.Text.Length > 0)
			{
				i = comboBox1.FindString(linkLabel2.Text);
				if(i < 0)
				{
					i = 0;
				}
			}
			comboBox1.SelectedIndex = i;

			if(linkLabel3.Text.Length > 0)
			{
				i = comboBox2.FindString(dep);
				if(i > 0)
				{
					comboBox2.SelectedIndex = i;
				}
			}

			if(linkLabel8.Text.Length > 0)
			{
				i = comboBox3.FindString(sect);
				if(i > 0)
				{
					comboBox3.SelectedIndex = i;
				}
			}
		}

		private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			int i;
			string dep = linkLabel3.Text;
			string sect = linkLabel8.Text;
			string find = linkLabel4.Text;

			i = 0;

			if(linkLabel2.Text.Length > 0)
			{
				i = comboBox1.FindString(linkLabel2.Text);
				if(i < 0)
				{
					i = 0;
				}
			}
			comboBox1.SelectedIndex = i;

			if(linkLabel3.Text.Length > 0)
			{
				i = comboBox2.FindString(dep);
				if(i > 0)
				{
					comboBox2.SelectedIndex = i;
				}
			}

			if(linkLabel8.Text.Length > 0)
			{
				i = comboBox3.FindString(sect);
				if(i > 0)
				{
					comboBox3.SelectedIndex = i;
				}
			}

			UpdateList(find);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			UpdateList(textBox1.Text);
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch((Keys)e.KeyChar)
			{
				case Keys.Enter:
					UpdateList(textBox1.Text);
					break;
			}
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Down:
				case Keys.Up:
					listView1.Focus();
					break;
			}
		}

		private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Down:
				case Keys.Up:
				case Keys.Enter:
					break;
				default:
					textBox1.Focus();
					break;
			}
		}

		private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			comboBox1.SelectedIndex = 0;
			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;
		}

		private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Form2 form2 = new Form2();

			if (comboBox1.SelectedIndex > 0)
			{
				form2.textBox6.Text = comboBox1.SelectedItem.ToString();
			}

			if (comboBox2.SelectedIndex > 0)
			{
				form2.textBox7.Text = comboBox2.SelectedItem.ToString();
			}

			if (comboBox3.SelectedIndex > 0)
			{
				form2.textBox8.Text = comboBox3.SelectedItem.ToString();
			}

			form2.button1.DialogResult = DialogResult.Cancel;

			if (form2.ShowDialog() == DialogResult.OK)
			{
				string[] row = new string[elements.Length];

				row[0] = form2.textBox1.Text;
				row[1] = form2.textBox2.Text;
				row[2] = form2.textBox3.Text;
				row[3] = form2.textBox4.Text;
				row[4] = form2.textBox5.Text;
				row[5] = form2.textBox6.Text;
				row[6] = form2.textBox7.Text;
				row[7] = form2.textBox8.Text;
				row[8] = form2.textBox9.Text;

				database.Add(row);

				linkLabel7.Visible = true;
				unsaved = true;
				UpdateList(textBox1.Text);
			}
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			string[] row;
			Form2 form2 = new Form2();

			if(listView1.SelectedItems.Count > 0)
			{
				row = (string[]) listView1.SelectedItems[0].Tag;
				form2.textBox1.Text = row[0];
				form2.textBox2.Text = row[1];
				form2.textBox3.Text = row[2];
				form2.textBox4.Text = row[3];
				form2.textBox5.Text = row[4];
				form2.textBox6.Text = row[5];
				form2.textBox7.Text = row[6];
				form2.textBox8.Text = row[7];
				form2.textBox9.Text = row[8];
				form2.button1.DialogResult = DialogResult.Cancel;

				if(form2.ShowDialog() == DialogResult.OK)
				{
					row[0] = form2.textBox1.Text;
					row[1] = form2.textBox2.Text;
					row[2] = form2.textBox3.Text;
					row[3] = form2.textBox4.Text;
					row[4] = form2.textBox5.Text;
					row[5] = form2.textBox6.Text;
					row[6] = form2.textBox7.Text;
					row[7] = form2.textBox8.Text;
					row[8] = form2.textBox9.Text;

					linkLabel7.Visible = true;
					unsaved = true;
					UpdateList(textBox1.Text);
				}
			}
		}

		private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			XmlTextWriter w = new XmlTextWriter(datafile, Encoding.Default);
			w.Formatting = Formatting.Indented;
			w.Indentation = 1;
			w.IndentChar = '\t';
			w.WriteStartDocument(true);
			w.WriteStartElement("phonebook");
			foreach(string[] row in database)
			{
				w.WriteStartElement("contact");
				int i = 0;
				foreach(string cell in row)
				{
					if ((cell != null) && (cell.Length > 0))
					{
						w.WriteStartElement(elements[i]);
						w.WriteString(cell);
						w.WriteEndElement();
					}
					i++;
				}
				w.WriteEndElement();
			}

			w.WriteEndElement();
			w.WriteEndDocument();
			w.Flush();
			w.Close();
			
			linkLabel7.Visible = false;
			unsaved = false;
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			AboutBox1 ab = new AboutBox1();

			ab.ShowDialog();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// hide at startup
			//Hide();
			//MessageBox.Show(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string cb1 = null;

			if (comboBox1.SelectedIndex > 0)
			{
				cb1 = comboBox1.SelectedItem.ToString();
			}

			comboBox2.Items.Clear();
			comboBox3.Items.Clear();

			comboBox2.Items.Add("Все департаменты");
			//comboBox3.Items.Add("Все отделы");

			foreach (string[] row in database)
			{
				// 0 ... 5 6 7
				if ((row.Length >= 7) && ((cb1 == null) || ((row[5] != null) && (row[5].CompareTo(cb1) == 0))))
				{
					if ((row[6] != null) && (comboBox2.FindString(row[6]) == -1))
					{
						comboBox2.Items.Add(row[6]);
					}
				}
			}

			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;

			label4.Text = (comboBox1.Items.Count - 1).ToString();

			//UpdateList(textBox1.Text);
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{

			string cb1 = null;
			string cb2 = null;

			if (comboBox1.SelectedIndex > 0)
			{
				cb1 = comboBox1.SelectedItem.ToString();
			}

			if (comboBox2.SelectedIndex > 0)
			{
				cb2 = comboBox2.SelectedItem.ToString();
			}

			comboBox3.Items.Clear();

			comboBox3.Items.Add("Все отделы");

			foreach (string[] row in database)
			{
				// 0 ... 5 6 7
				if ((row.Length >= 8) && ((cb1 == null) || ((row[5] != null) && (row[5].CompareTo(cb1) == 0))) && ((cb2 == null) || ((row[6] != null) && (row[6].CompareTo(cb2) == 0))))
				{
					if ((row[7] != null) && (comboBox3.FindString(row[7]) == -1))
					{
						comboBox3.Items.Add(row[7]);
					}
				}
			}

			comboBox3.SelectedIndex = 0;

			label9.Text = (comboBox2.Items.Count - 1).ToString();

			//UpdateList(textBox1.Text);
		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			label10.Text = (comboBox3.Items.Count - 1).ToString();

			UpdateList(textBox1.Text);
		}

		private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if(!unsaved || (MessageBox.Show("Несохраненные данные будут потеряны. Продолжить?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
			{
				LoadBase();
				UpdateList(null);
				unsaved = false;
				linkLabel7.Visible = false;
			}
		}

		private void LoadBase()
		{
			//*
			if((datafile == null) || !File.Exists(datafile))
			{
				MessageBox.Show("Can't open database file.");

				datafile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\base.xml";

				FolderBrowserDialog fd = new FolderBrowserDialog();
				fd.Description = "Выберите папку, где находится база данных (base.xml)";
				
				do
				{
					// get dir
					if(fd.ShowDialog() == DialogResult.OK)
					{
						datafile = fd.SelectedPath + "\\base.xml";
						//MessageBox.Show(datafile);
					}
					else
					{
						break;
					}
					//datafile = path + "base.xml";
				} while(!File.Exists(datafile));

				XmlTextWriter w = new XmlTextWriter(config, Encoding.Default);
				w.Formatting = Formatting.Indented;
				w.Indentation = 1;
				w.IndentChar = '\t';
				w.WriteStartDocument(true);
				w.WriteStartElement("config");

				w.WriteStartElement("basefile");
				w.WriteString(datafile);
				w.WriteEndElement();

				w.WriteEndElement();
				w.WriteEndDocument();
				w.Flush();
				w.Close();
			}

			if (!File.Exists(datafile))
            {
                MessageBox.Show("Can't open database file.\n" + datafile);
            }
            else
			//*/
            {
                /*
                FileStream hFile = File.Open("D:\\test.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                byte[] buf = new byte[8768];
                hFile.Read(buf, 0, 8768);
                hFile.Close();

                System.Text.ASCIIEncoding cEnc = new System.Text.ASCIIEncoding();

                string str = cEnc.GetString(buf, 0, 10);
                MessageBox.Show(str);
                */
                string[] row = null;
				database.Clear();

                int column = 0;

				comboBox1.Items.Clear();
				comboBox1.Items.Add("Все организации");
				/*
				comboBox2.Items.Add("Все департаменты");
				comboBox3.Items.Add("Все отделы");

				comboBox1.SelectedIndex = 0;
				comboBox2.SelectedIndex = 0;
				comboBox3.SelectedIndex = 0;
				*/
				
				FileStream stream = new FileStream(datafile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			/*
			lb_load:
				try
				{
					HttpWebRequest request = (HttpWebRequest) WebRequest.Create(datafile);
					HttpWebResponse response = (HttpWebResponse) request.GetResponse();
					Stream stream = response.GetResponseStream();
			//*/

					System.Xml.XmlReader reader = new System.Xml.XmlTextReader(stream);
					//System.Xml.XmlReader reader = new System.Xml.XmlTextReader("http://localhost/base.xml");

					while(reader.Read())
					{
						switch(reader.NodeType)
						{
							case XmlNodeType.Element:
								column = 0;
								if(reader.Name.CompareTo("contact") == 0)
								{
									row = new string[elements.Length];
								}
								else
								{
									int i = 1;
									foreach(string element in elements)
									{
										if(element.CompareTo(reader.Name) == 0)
										{
											column = i;
											break;
										}
										i++;
									}
								}
								break;
							case XmlNodeType.Text:
								if((column > 0) && (row != null))
								{
									row[column - 1] = reader.Value;
								}
								switch(column)
								{
									case 6:
										if(comboBox1.FindString(reader.Value) == -1)
										{
											comboBox1.Items.Add(reader.Value);
										}
										break;
									/*
										case 7:
											if (comboBox2.FindString(reader.Value) == -1)
											{
												comboBox2.Items.Add(reader.Value);
											}
											break;
										case 8:
											if (comboBox3.FindString(reader.Value) == -1)
											{
												comboBox3.Items.Add(reader.Value);
											}
											break;
									*/
								}
								break;
							case XmlNodeType.EndElement:
								if(reader.Name.CompareTo("contact") == 0)
								{
									if(row != null)
									{
										database.Add(row);
									}
								}
								break;
						}
					}
					reader.Close();
					stream.Close();

					comboBox1.SelectedIndex = 0;
				/*
				}

				catch
				{
					Form4 form4 = new Form4();

					form4.textBox1.Text = datafile;

					if(form4.ShowDialog() == DialogResult.OK)
					{
						datafile = form4.textBox1.Text;
						XmlTextWriter w = new XmlTextWriter(config, Encoding.Default);
						w.Formatting = Formatting.Indented;
						w.Indentation = 1;
						w.IndentChar = '\t';
						w.WriteStartDocument(true);
						w.WriteStartElement("config");

						w.WriteStartElement("basefile");
						w.WriteString(datafile);
						w.WriteEndElement();

						w.WriteEndElement();
						w.WriteEndDocument();
						w.Flush();
						w.Close();

						goto lb_load;
					}
				}
				//*/
			}
		}
    }
}
