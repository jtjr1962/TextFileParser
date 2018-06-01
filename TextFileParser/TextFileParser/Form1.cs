//
// Description:
//   Opens a text type file
//   Inverts case 
//   Displays to rich text box on screen 
//   Saves output to new file with out.txt appended to filename
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFileParser
{
	public partial class Form1 : Form
	{
		unsafe char* winfile_name_ptr = null;
		OpenFileDialog openFileDialog1 = new OpenFileDialog();
		public Form1()
		{
			InitializeComponent();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "Text Files (*.txt)|*.txt|(*.asc)|*.asc";
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.FilterIndex = 1;  // correlates to *.txt; if value was 2 it would correlate to *.asc (2nd in list above)
			openFileDialog1.RestoreDirectory = true;

			// Show the dialog and get result.
			try
			{
				// "How to do basic file I/O in Visual C#"      https://support.microsoft.com/en-us/kb/304430

				DialogResult result = openFileDialog1.ShowDialog();
				if (result == DialogResult.OK) // Test result.
				{
					try
					{
                        progressBar1.Value = 0;
								// Insert code to read the stream here.
								//Use Image.ImageFromFile http://msdn.microsoft.com/en-us/library/system.drawing.image.fromfile.aspx method
								//// Dock the PictureBox to the form and set its background to white.
								//pictureBox2.Dock = DockStyle.Fill;
								//pictureBox2.BackColor = Color.White;

								////Image img = Image.ImageFromFile(openFileDialog1.FileName);
//                                  Image img = Image.FromFile(openFileDialog1.FileName);

								//pictureBox2.Image = img;


					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
					}
				}
				Console.WriteLine(result); // <-- For debugging use.
			}
			catch
			{
				MessageBox.Show("Error Opening File");

			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
// debug only			MessageBox.Show("Converting");

			// Example #2
			// Read each line of the file into a string array. Each element
			// of the array is one line of the file.
			string[] allLinesInFile = System.IO.File.ReadAllLines(openFileDialog1.FileName);

			string inputFileName = openFileDialog1.FileName;
			string outputFileName = inputFileName + "out.txt";


            // clear the Rich Text Box on entry
            richTextBox1.Clear();

// debug only			MessageBox.Show(inputFileName);

			// This line will write entire file to the RichTextBox on Form

            // NOPE only write parsed output            richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);

            // Reset progress bar value
            progressBar1.Value = 0;

            // Sets the progress bar's minimum value to a number representing
			// no operations complete -- in this case, no files read.
			progressBar1.Minimum = 0;
			// Sets the progress bar's maximum value to a number representing
			// all operations complete -- in this case, all five files read.
			long length = new System.IO.FileInfo(openFileDialog1.FileName).Length;
			progressBar1.Maximum = (int)length;

			// Sets the Step property to amount to increase with each iteration.
			// In this case, it will increase by one with every file read.
			progressBar1.Step = 1;

			try
			{
				System.IO.StreamWriter file = new System.IO.StreamWriter(outputFileName);

			    // Display String to Consolefile contents by using a foreach loop.
			    foreach (string line in allLinesInFile)
			    {
				    System.Text.StringBuilder outputLine = new System.Text.StringBuilder(line);

				    // parse each line
				    for (int j = 0; j < outputLine.Length; j++)
				    {
					    if (System.Char.IsLower(outputLine[j]) == true)
					    {
						    outputLine[j] = System.Char.ToUpper(outputLine[j]);
					    }
					    else if (System.Char.IsUpper(outputLine[j]) == true)
					    {
						    outputLine[j] = System.Char.ToLower(outputLine[j]);
					    }

				    }

				    // Update progress bar step, file size is in bytes, but probably all unicode chars, * 2 for wchars...
				    progressBar1.Step = line.Length*2;
				    // Update progress bar display
				    progressBar1.PerformStep();

				    // Convert StringBuilder string to regular string for output purposes
				    string modifiedOutputString = outputLine.ToString();
				    richTextBox1.AppendText(modifiedOutputString);

				    // write each line to output file
				    file.WriteLine(modifiedOutputString);

			    }

                MessageBox.Show("File Converted");
                
			}
			catch
			{
				MessageBox.Show("Error creating output file, ensure input/output directory is writeable by mere users");
			}

            // Display String to Console (Output)
            System.Console.WriteLine("Output/Console Message...\t ");

		}



		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}
	}
}
