using CSCloseOut.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSCloseOut.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/CloseOut")]
    public class CloseOutController : ApiController
    {
        private int idIndex = 1;
        const float SATURATION = 0.55f;
        const float BRIGHTNESS = 0.53f;
        const float HUE = 360f;
        const int HUESTEP = 5;


        [HttpGet]
        [Route("CloseOutOptions")]
        public CloseOutOption[] GetCloseOutOptions()
        {
            List<CloseOutOption> options = new List<CloseOutOption>();

            options.Add(this.BuildRegCloseOutOptions());
            options.Add(this.BuildHousingCloseOutOptions());
            options.Add(this.BuildLeadCloseOutOptions());

            return options.ToArray();
        }

        #region HousingOptions
        private string[] houWebCodes = new string[] { 
            "WEBTRANS",
            "ERRORMSG",
            "LOGIN",
            "DUPELOGIN"};
        private string[] houHotelCodes = new string[]{
            "HOTELDATES",
            "PREFHOTEL",
            "HOTELCHANGEDATES",
            "HOTELPRICING",
            "COMPLETERES",
            "HOTELWAITLIST",
            "HOTELCANCELRES",
            "CONFIRMRES"};
        private string[] houCodes = new string[]{
            "EMAILCONF",
            "DUPERES",
            "EMAILRESPONSE",
            "REGBEFORERES",
            "GPCTRANSFER",
            "GPCESCALATE"
        };
        private string[] houWebDescs = new string[]{
            "Unable to complete web transaction",
            "Site error message",
            "Login/Password Problem",
            "Duplicated login - cxld to complete transaction"};
        private string[] houHotelDescs = new string[]{
            "Dates not available ",
            "Preferred Hotel not available",
            "Change dates over phone",
            "Pricing/availability over phone",
            "Complete Hotel Reservation on phone",
            "Waitlist Inquiry",
            "Hotel Cancel Reservation - Referred to Email",
            "Confirm Hotel reservation"};
        private string[] houDescs = new string[]{
            "Confirmation email request",
            "Duplicate Reservation URGO sent to guest",
            "Response to email sent - Duplicate reservation on file/Waitlist Not Avail/Declined CC/Hotel Move",
            "Registration required before housing - referred to online",
            "GPC Transfer",
            "GPC Escalation"};

        private CloseOutOption BuildHousingCloseOutOptions()
        {
            var housingOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "HOU",
                Description = "Housing",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };

            var webOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "WEB",
                Description = "Web",
                Color = Color.OliveDrab
            };
            for (int i = 0; i < houWebDescs.Length; i++)
            {
                webOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = houWebCodes[i],
                    Description = houWebDescs[i]
                });
            }
            housingOption.AddChildOption(webOption);

            var hotelOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "HOTEL",
                Description = "Hotel",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex) % HUE, SATURATION, BRIGHTNESS)
            };
            for (int i = 0; i < houHotelDescs.Length; i++)
            {
                hotelOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = houHotelCodes[i],
                    Description = houHotelDescs[i]
                });
            }
            housingOption.AddChildOption(hotelOption);

            for (int i = 0; i < houDescs.Length; i++)
            {
                housingOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = houCodes[i],
                    Description = houDescs[i]
                });
            }

            return housingOption;
        }
        #endregion

        #region RegOptions
        private string[] regWebCodes = new string[] {
            "WEBTRANS",
            "ERRORMSG",
            "LOGIN",
            "DUPELOGIN"
        };
        private string[] regWebDescs = new string[] { 
            "Unable to complete web transaction",
            "Site error message",
            "Login/Password Problem",
            "Duplicated login - cxld to complete transaction"
        };

        private string[] regAssistCodes = new string[] {
            "PROMOCODE",
            "BADGE",
            "APPROVAL",
            "EXHGUEST"
        };
        private string[] regAssistDescs = new string[] { 
            "Promo Code Assistance",
            "Badge Assistance",
            "Credentials/Approval Assistance",
            "Exhibitor Guest Pass Assistance"
        };

        private string[] regShowRegCodes = new string[] { 
            "CANCELSUB",
            "CHANGEREG",
            "ADDSESSION",
            "NOPHONEREG",
            "EXHSTAFF"
        };
        private string[] regShowRegDescs = new string[] { 
            "Cancel/Substitute Registration - Referred to Email",
            "Change Registration (Regtype or Demographic)",
            "Add Class/Sessions to Registration",
            "Phone Registration not Allowed - Referred to Online",
            "Register Exhibitor Booth Staff"
        };

        private string[] regRequestCodes = new string[] {
            "CONFEMAIL",
            "INVOICE"
        };
        private string[] regRequestDescs = new string[] { 
            "Confirmation email request",
            "Invoice Request"
        };

        private string[] regCodes = new string[] {
            "PRICING",
            "VENUE",
            "NONBUYER",
            "DENIED",
            "CHECKFAXMAIL"
        };
        private string[] regDescs = new string[]
        {
            "Pricing Only Request - No Registration",
            "General Venue Questions (location, amenities, transportation)",
            "Non-Buyer/Non-Exhibitor Dispute",
            "Denied access to show - Escalated",
            "Check/Fax/Mail received"
        };

        private CloseOutOption BuildRegCloseOutOptions()
        {
            var regOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "REG",
                Description = "Registration",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };

            var regWebOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "WEB",
                Description = "Web",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };
            for (int i = 0; i < regWebDescs.Length; i++)
            {
                regWebOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = regWebCodes[i],
                    Description = regWebDescs[i]
                });
            }
            regOption.AddChildOption(regWebOption);

            var regAssistOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "ASSIST",
                Description = "Assistance",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };
            for (int i = 0; i < regAssistDescs.Length; i++)
            {
                regAssistOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = regAssistCodes[i],
                    Description = regAssistDescs[i]
                });
            }
            regOption.AddChildOption(regAssistOption);

            var regShowRegOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "SHOWREG",
                Description = "Show Registration",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };
            for (int i = 0; i < regShowRegDescs.Length; i++)
            {
                regShowRegOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = regShowRegCodes[i],
                    Description = regShowRegDescs[i]
                });
            }
            regOption.AddChildOption(regShowRegOption);

            var regRequestOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "REQUEST",
                Description = "Requests",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };
            for (int i = 0; i < regRequestDescs.Length; i++)
            {
                regRequestOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = regRequestCodes[i],
                    Description = regRequestDescs[i]
                });
            }
            regOption.AddChildOption(regRequestOption);

            for (int i = 0; i < regDescs.Length; i++)
            {
                regOption.AddChildOption(new CloseOutOption()
                {
                    ID = idIndex++,
                    Code = regCodes[i],
                    Description = regDescs[i]
                });
            }

            return regOption;
        }
        #endregion

        private CloseOutOption BuildLeadCloseOutOptions()
        {
            var leadOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "LEAD",
                Description = "Lead Sales",
                Color = ColorChanger.ColorFromAhsb(1, (HUESTEP + idIndex), SATURATION, BRIGHTNESS)
            };

            var swapOption = new CloseOutOption()
            {
                ID = idIndex++,
                Code = "SWAP",
                Description = "SWAP issues"
            };
            leadOption.AddChildOption(swapOption);

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "PRODUCTINFO",
                Description = "Product Info"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "PLACEORDER",
                Description = "Place Order"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "CHANGEORDER",
                Description = "Change Order"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "PAYMENT",
                Description = "Make Payment"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "REFUND",
                Description = "Refund Request"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "ONSITE",
                Description = "Onsite Assistance"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "PORTAL",
                Description = "Portal Login Issues"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = idIndex++,
                Code = "REFERRAL",
                Description = "Referral to Rep"
            });
            return leadOption;
        }
    }
}
