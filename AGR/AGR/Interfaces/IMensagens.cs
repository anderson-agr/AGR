using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGR.Interfaces
{
 public interface IMensagens
    {
        //
        // Summary:
        //     Displays a native platform action sheet, allowing the application user to choose
        //     from several buttons.
        //
        // Parameters:
        //   title:
        //     Title of the displayed action sheet. Must not be null.
        //
        //   cancel:
        //     Text to be displayed in the 'Cancel' button. Can be null to hide the cancel action.
        //
        //   destruction:
        //     Text to be displayed in the 'Destruct' button. Can be null to hide the destructive
        //     option.
        //
        //   buttons:
        //     Text labels for additional buttons. Must not be null.
        //
        // Returns:
        //     An awaitable Task that displays an action sheet and returns the Text of the button
        //     pressed by the user.
        //
        // Remarks:
        //     To be added.
         Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
        //
        // Summary:
        //     Presents an alert dialog to the application user with a single cancel button.
        //
        // Parameters:
        //   title:
        //     The title of the alert dialog.
        //
        //   message:
        //     The body text of the alert dialog.
        //
        //   cancel:
        //     Text to be displayed on the 'Cancel' button.
        //
        // Returns:
        //     To be added.
        //
        // Remarks:
        //     To be added.
         Task DisplayAlert(string title, string message, string cancel);
        //
        // Summary:
        //     Presents an alert dialog to the application user with an accept and a cancel
        //     button.
        //
        // Parameters:
        //   title:
        //     The title of the alert dialog.
        //
        //   message:
        //     The body text of the alert dialog.
        //
        //   accept:
        //     Text to be displayed on the 'Accept' button.
        //
        //   cancel:
        //     Text to be displayed on the 'Cancel' button.
        //
        // Returns:
        //     To be added.
        //
        // Remarks:
        //     To be added.
         Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    }
}
