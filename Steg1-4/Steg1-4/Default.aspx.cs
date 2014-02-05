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
        //Hantering av SecretNumber objekt från Session
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
                    //Objektet finns inte i session, skapa det.
                    Session["sn"] = new SecretNumber();
                    return (SecretNumber)Session["sn"];
                }
            }
        }
        //Page load, sätter fokus på textbox och aktiverar validering för tomt fält.
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxGuess.Focus();
            RequiredFieldValidator1.Enabled = true;
        }

        //Presentationslogik för hemliga talet
        protected void ButtonGuess_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Hämtar resultat för gissningen
                OutCome result = sn.MakeGuess(int.Parse(TextBoxGuess.Text));

                //Presenterar tidigare gissningar
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
                //Kontrollerar antalet gissningar som gjorts
                if (sn.Count == 7)
                {
                    LiteralFail.Text = "Du har inga gissningar kvar, det hemliga talet var " + sn.Number + ".";
                    GameOver();
                }
            }
        }

        //Omstart av spelet, återaktiverar deaktiverade kontroller och initierar secretnumber
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

        //Presentationslogik för när ett spel är slut
        private void GameOver()
        {
            TextBoxGuess.Enabled = false;
            ButtonGuess.Enabled = false;
            ButtonNewSecretNumber.Enabled = true;
            ButtonNewSecretNumber.Visible = true;
        }
    }
}