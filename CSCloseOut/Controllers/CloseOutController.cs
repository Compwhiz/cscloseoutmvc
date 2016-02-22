using CSCloseOut.Models;
using System;
using System.Collections.Generic;
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

        private CloseOutOption BuildRegCloseOutOptions()
        {
            var regOption = new CloseOutOption()
            {
                ID = 1,
                Code = "REG",
                Description = "Registration"
            };



            return regOption;
        }

        private CloseOutOption BuildHousingCloseOutOptions()
        {
            var housingOption = new CloseOutOption()
            {
                ID = 2,
                Code = "HOU",
                Description = "Housing"
            };

            return housingOption;
        }

        private CloseOutOption BuildLeadCloseOutOptions()
        {
            var leadOption = new CloseOutOption()
            {
                ID = 3,
                Code = "LEAD",
                Description = "Lead Sales"
            };

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 31,
                Code = "SWAP",
                Description = "SWAP issues"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 32,
                Code = "PRODUCTINFO",
                Description = "Product Info"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 33,
                Code = "PLACEORDER",
                Description = "Place Order"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 34,
                Code = "CHANGEORDER",
                Description = "Change Order"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 35,
                Code = "PAYMENT",
                Description = "Make Payment"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 36,
                Code = "REFUND",
                Description = "Refund Request"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 37,
                Code = "ONSITE",
                Description = "Onsite Assistance"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 38,
                Code = "PORTAL",
                Description = "Portal Login Issues"
            });

            leadOption.AddChildOption(new CloseOutOption()
            {
                ID = 39,
                Code = "REFERRAL",
                Description = "Referral to Rep"
            });
            return leadOption;
        }
    }
}
