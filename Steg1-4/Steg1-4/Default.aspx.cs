using Steg1_4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Steg1_4
{
    public partial class _Default : Page
    {
        private SecretNumber sn
        {
            get
            {
                if (Session["sn"] != null)
                {
                    return (SecretNumber)Session["sn"];
                }
                else
                {
                    Session["sn"] = new SecretNumber();
                    return (SecretNumber)Session["sn"];
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxGuess.Focus();
            RequiredFieldValidator1.Enabled = true;
        }

        protected void ButtonGuess_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                OutCome result = sn.MakeGuess(int.Parse(TextBoxGuess.Text));

                LiteralGuesses.Text = string.Join(", ", sn.PreviousGuesses);

                switch (result)
                {
                    case OutCome.Indefinite:
                        LiteralOutcome.Text = "Ingen gissning gjord.";
                        break;
                    case OutCome.Low:
                        LiteralOutcome.Text = "För lågt.";
                        break;
                    case OutCome.High:
                        LiteralOutcome.Text = "För Högt.";
                        break;
                    case OutCome.Correct:
                        LiteralOutcome.Text = "Grattis! Du klarade det på " + sn.Count + " försök.";
                        GameOver();
                        break;
                    case OutCome.NoMoreGuesses:
                        throw new ApplicationException("Detta borde aldrig inträffa");
                    case OutCome.PreviousGuess:
                        LiteralOutcome.Text = "Du har redan gissat på detta tal.";
                        break;
                    default:
                        throw new ApplicationException("Något gick snett");
                }
                if (sn.Count == 7)
                {
                    LiteralFail.Text = "Du har inga gissningar kvar, det hemliga talet var " + sn.Number + ".";
                    GameOver();
                }
            }
        }

        protected void ButtonNewSecretNumber_Click(object sender, EventArgs e)
        {
            sn.Initialize();
            TextBoxGuess.Enabled = true;
            ButtonGuess.Enabled = true;
            ButtonNewSecretNumber.Enabled = false;
            ButtonNewSecretNumber.Visible = false;
            RequiredFieldValidator1.Enabled = false;
            Response.Redirect(Request.RawUrl);
        }

        private void GameOver()
        {
            TextBoxGuess.Enabled = false;
            ButtonGuess.Enabled = false;
            ButtonNewSecretNumber.Enabled = true;
            ButtonNewSecretNumber.Visible = true;
        }
    }
}