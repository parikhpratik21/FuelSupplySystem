using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace FuelSupply.BAL.Manager.Common
{
    public class MessageManager
    {
        #region "Method"

        #region "Method: ShowErrorMessage(2)"
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="pMessage">The p message.</param>
        /// <param name="pTitle">The p title.</param>
        /// <param name="pButton">The p button.</param>
        /// <param name="pIcon">The p icon.</param>
        public static void ShowErrorMessage(string pMessage, System.Windows.Window pParentWindow) // Error
        {
            MessageBox.Show(pParentWindow, pMessage, Constant.MESSAGE_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            //MessageBox.Show(pParentWindow,pMessage, Constant.MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region "Method: ShowWarningMessage(2)"
        /// <summary>
        /// Shows the warning message.
        /// </summary>
        /// <param name="pMessage">The p message.</param>
        /// <param name="pTitle">The p title.</param>
        /// <param name="pButton">The p button.</param>
        /// <param name="pIcon">The p icon.</param>
        public static void ShowWarningMessage(string pMessage, System.Windows.Window pParentWindow) // Warning
        {
            MessageBox.Show(pParentWindow, pMessage, Constant.MESSAGE_TITLE, MessageBoxButton.OK, MessageBoxImage.Warning);
            //MessageBox.Show(pMessage, Constant.MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);     
        }

        #endregion

        #region "Method: ShowInformationMessage(2)"
        /// <summary>
        /// Shows the information message.
        /// </summary>
        /// <param name="pMessage">The p message.</param>
        /// <param name="pTitle">The p title.</param>
        /// <param name="pButton">The p button.</param>
        /// <param name="pIcon">The p icon.</param>
        public static void ShowInformationMessage(string pMessage, System.Windows.Window pParentWindow) // Information
        {
            MessageBox.Show(pParentWindow, pMessage, Constant.MESSAGE_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
            //MessageBox.Show(pMessage, Constant.MESSAGE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);     
        }

        #endregion

        #region "Method: ShowConfirmationMessage(2)"
        /// <summary>
        /// Shows the information message.
        /// </summary>
        /// <param name="pMessage">The p message.</param>
        /// <param name="pTitle">The p title.</param>
        /// <param name="pButton">The p button.</param>
        /// <param name="pIcon">The p icon.</param>
        public static bool ShowConfirmationMessage(string pMessage, System.Windows.Window pParentWindow) // Question
        {
            //MessageBox.Show(pMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 

            if (MessageBoxResult.Yes == MessageBox.Show(pMessage, Constant.MESSAGE_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region "Method: ShowErrorConfirmationMessage(2)"
        /// <summary>
        /// Shows Error the confirmation message.
        /// </summary>
        /// <param name="pMessage">The p message.</param>
        /// <param name="pTitle">The p title.</param>
        /// <param name="pButton">The p button.</param>
        /// <param name="pIcon">The p icon.</param>
        public static bool ShowErrorConfirmationMessage(string pMessage, System.Windows.Window pParentWindow) // Question
        {
            if (MessageBoxResult.Yes == MessageBox.Show(pMessage, Constant.MESSAGE_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Error))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}
