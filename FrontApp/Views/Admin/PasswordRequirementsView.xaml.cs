using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontApp.Views.Admin;

public partial class PasswordRequirementsView : ContentPage
{
    public PasswordRequirementsView()
    {
        InitializeComponent();
        SmallLettersCheckBox.IsChecked = PasswordRequirements.IsSmallLetters;
        BigLettersCheckBox.IsChecked = PasswordRequirements.IsBigLetters;
        SpecialLettersCheckBox.IsChecked = PasswordRequirements.IsSpecialLetters;
        PasswordLengthLabel.Text = $"długość: {PasswordRequirements.PasswordLength}";
        PasswordExpirationLabel.Text = $"Ilość dni do wygaśnięcia: {PasswordRequirements.PasswordExpiration}";
    }

    private void ChangePasswordRequirements(object sender, CheckedChangedEventArgs e)
    {
        PasswordRequirements.IsSmallLetters = SmallLettersCheckBox.IsChecked;
        PasswordRequirements.IsBigLetters = BigLettersCheckBox.IsChecked;
        PasswordRequirements.IsSpecialLetters = SpecialLettersCheckBox.IsChecked;
    }

    private void ChangePasswordExpiration(object sender, ValueChangedEventArgs e)
    {
        PasswordRequirements.PasswordLength = (int)PasswordLengthStepper.Value;
        PasswordRequirements.PasswordExpiration = TimeSpan.FromDays((int)PasswordExpirationStepper.Value);
        PasswordLengthLabel.Text = $"długość: {PasswordRequirements.PasswordLength}";
        PasswordExpirationLabel.Text = $"Ilość dni do wygaśnięcia: {PasswordRequirements.PasswordExpiration}";
    }
}