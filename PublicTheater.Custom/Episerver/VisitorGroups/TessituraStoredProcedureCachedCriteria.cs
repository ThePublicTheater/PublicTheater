using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Data.Dynamic;
using EPiServer.Data;
using EPiServer.Personalization.VisitorGroups;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web.Mvc.VisitorGroups;
using Adage.TessituraEpiServer;

namespace PublicTheater.Custom.Episerver.VisitorGroups
{

    [EPiServer.Personalization.VisitorGroups.VisitorGroupCriterion(
        Category = "Tessitura Criteria",
        DisplayName = "Stored Procedure Cached")]
    public class TessituraStoredProcedureCachedCriteria : CriterionBase<TessituraStoredProcedureCachedModel>
    {
        public override bool IsMatch(System.Security.Principal.IPrincipal principal, HttpContextBase httpContext)
        {
            return TessituraVisitorCriterion.GetSelector().CheckFilter(Model, httpContext);
        }
    }

    public class TessituraStoredProcedureCachedModel : TessituraVisitorModelBase
    {
        protected override UserFilterTypes UserFilterType
        {
            get { return (UserFilterTypes) PublicTessituraVisitorCriterionSelector.PublicTheaterUserFilterTypes.CachedStoredProc; }
            set { }
        }

        [DojoWidget(
            LabelTranslationKey = "/enums/adage/tessituraepiserver/userfiltertypes/storedprocedurecached"),
            Required]
        public override string UserFilterCondition1 { get; set; }
    }
}