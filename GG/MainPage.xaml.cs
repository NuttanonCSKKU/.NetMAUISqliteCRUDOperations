namespace GG
{
    public partial class MainPage : ContentPage
    {
       private readonly LocalDBService _dbService;
        private int _editCustomerId;

        public MainPage(LocalDBService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
          Task.Run(async() => listView.ItemsSource = await _dbService.GetCustomers());

        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (_editCustomerId == 0)
            {
                await _dbService.Create(new Customer
                {
                    CustomerName = nameEntryField.Text,
                    Email = nameEntryField.Text,
                    Mobile = mobileEntryField.Text,
                });
            }
            else
            {
                // Update an existing customer
                await _dbService.Update(new Customer
                {
                    Id = _editCustomerId,
                    CustomerName = nameEntryField.Text,
                    Email = emailEntryFied.Text, // Fixed incorrect field name
                    Mobile = mobileEntryField.Text,
                });
                _editCustomerId = 0; // Reset the edit ID
            }
            nameEntryField.Text = string.Empty;
            emailEntryFied.Text = string.Empty;
            mobileEntryField.Text = string.Empty;

            listView.ItemsSource = await _dbService.GetCustomers();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e) {
            var customer =(Customer)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action) 
            {
                case "Edit":

                    _editCustomerId = customer.Id;
                    nameEntryField.Text = customer.CustomerName;
                    emailEntryFied.Text= customer.Email;
                    mobileEntryField.Text = customer.Mobile;

                    break;
                case "Delete":

                    await _dbService.Delete(customer);
                    listView.ItemsSource = await _dbService.GetCustomers();

                    break;
            }

        }
    }

}
