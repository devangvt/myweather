using System.Windows.Controls;
using System.Windows.Input;

namespace DevangsWeather.FindCity.Views
{
    /// <summary>
    /// Interaction logic for FindAndAddCity
    /// </summary>
    public partial class FindAndAddCity : UserControl
    {
        public FindAndAddCity()
        {
            InitializeComponent();
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.Key == Key.Enter)
                SearchButton.Command.Execute(textBox.Text);
        }
    }
}
