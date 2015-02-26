using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using Adage.TessituraEpiServer;
using EPiServer.Personalization.VisitorGroups;

namespace PublicTheater.Custom.Episerver.VisitorGroups
{
    [EPiServer.Personalization.VisitorGroups.VisitorGroupCriterion(
    Category = "Tessitura Criteria",
    DisplayName = "Cart Contains Flex Package")]
    public class CartContainsFlexPackageCriteria : CriterionBase<CarContainsFlexPackageModel>
    {
        public override bool IsMatch(System.Security.Principal.IPrincipal principal, HttpContextBase httpContext)
        {
            return TessituraVisitorCriterion.GetSelector().CheckFilter(Model, httpContext);
        }
    }

    public class CarContainsFlexPackageModel : TessituraVisitorModelBase
    {
        protected override UserFilterTypes UserFilterType
        {
            get { return (UserFilterTypes)PublicTessituraVisitorCriterionSelector.PublicTheaterUserFilterTypes.CartContainsFlexPackage; }
            set { }
        }

        [DojoWidget(
            LabelTranslationKey = "/enums/adage/tessituraepiserver/userfiltertypes/CartContainsFlexPackage"),
            Required]
        public override string UserFilterCondition1 { get; set; }
    }
}
