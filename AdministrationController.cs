[AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Agency Types")]
        public ActionResult AgencyTypes()
        {
            List<DataItem> agencyTypes = LookUpProvider.GetAgencyTypes(false, null);
            agencyTypes.Insert(0, new DataItem() { Id = -1, Name = "- Select Agency Type -", Value = "0" });
            ViewData["AgencyTypes"] = new SelectList(agencyTypes, "Id", "Name");

            List<DataItem> industryTypes = LookUpProvider.GetIndustryTypes();
            industryTypes.Insert(0, new DataItem() { Id = -1, Name = "- Select Industry Type -", Value = "0" });
            ViewData["IndustryTypes"] = new SelectList(industryTypes, "Id", "Name");

            //Set view permissions for aspx
            MenuSecurityModel model = ControllerHelper.ConvertToMenuSecurityModel(UserProvider.GetViewPagePermission(((ClaimsIdentity)HttpContext.User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray()));

            return View(model);


        }

        [AuthorizeUserAttribute(Access = "Delete", ViewPage = "Manage Agency Types")]
        public JsonResult DeleteAgencyType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, object> deleteResult = LookUpProvider.DeleteAgencyType(id);
            if ((LookUpProvider.LookUpResponseCode)deleteResult["Result"] != LookUpProvider.LookUpResponseCode.Success)
            {
                result.Add("ErrorMessage", deleteResult["ErrorMessage"].ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Agency Types")]
        public JsonResult GetAgencyType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            AgencyType agencyType = LookUpProvider.GetAgencyType(id, null);
            if (agencyType != null)
            {
                result.Add("Name", agencyType.Name);
                result.Add("SortOrder", agencyType.SortOrder.Value.ToString());
                result.Add("AllCertificates", agencyType.AllCertificates.ToString());
                result.Add("IndustryTypeId", agencyType.IndustryTypeId.ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Create, Update", ViewPage = "Manage Agency Types")]
        public JsonResult UpdateAgencyType(int id, string name, string allCertificates, int industryTypeId)
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            IDictionary<string, object> saveResult = LookUpProvider.SaveAgencyType(id, name, bool.Parse(allCertificates), industryTypeId);
            if ((LookUpProvider.LookUpResponseCode)saveResult["Result"] == LookUpProvider.LookUpResponseCode.Success)
            {
                List<DataItem> agencyTypes = LookUpProvider.GetAgencyTypes(false, null);
                agencyTypes.ToList().ForEach(t =>
                {
                    result.Add(t.Id, t.Name);
                });
            }
            return Json(result.ToArray<KeyValuePair<int, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Create, Update", ViewPage = "Manage Agency Types")]
        public JsonResult AddAgencyTypesToCertificates(int agencyTypeId)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            IDictionary<string, object> saveResult = LookUpProvider.AddAgencyTypeToCertificates(agencyTypeId);
            if ((LookUpProvider.LookUpResponseCode)saveResult["Result"] != LookUpProvider.LookUpResponseCode.Success)
            {
                result.Add("Error", saveResult["Result"].ToString());
                result.Add("ErrorMessage", saveResult["ErrorMessage"].ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }




[AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Debt Types")]
        public ActionResult DebtTypes()
        {
            List<DataItem> debtTypes = LookUpProvider.GetDebtTypes();
            debtTypes.Insert(0, new DataItem() { Id = -1, Name = "- Select Debt Type -", Value = "0" });
            ViewData["DebtTypes"] = new SelectList(debtTypes, "Id", "Name");

            //Set view permissions for aspx
            MenuSecurityModel model = ControllerHelper.ConvertToMenuSecurityModel(UserProvider.GetViewPagePermission(((ClaimsIdentity)HttpContext.User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray()));

            return View(model);
        }

        [AuthorizeUserAttribute(Access = "Delete", ViewPage = "Manage Debt Types")]
        public JsonResult DeleteDebtType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, object> deleteResult = LookUpProvider.DeleteDebtType(id);
            if ((LookUpProvider.LookUpResponseCode)deleteResult["Result"] != LookUpProvider.LookUpResponseCode.Success)
            {
                result.Add("ErrorMessage", deleteResult["ErrorMessage"].ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }


        [AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Debt Types")]
        public JsonResult GetDebtType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            DebtType debtType = LookUpProvider.GetDebtType(id, null);
            if (debtType != null)
            {
                result.Add("Name", debtType.Name);
                result.Add("AllCertificates", debtType.AllCertificates.ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Create, Update", ViewPage = "Manage Debt Types")]
        public JsonResult UpdateDebtType(int id, string name, string allCertificates)
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            IDictionary<string, object> saveResult = LookUpProvider.SaveDebtType(id, name, bool.Parse(allCertificates));
            if ((LookUpProvider.LookUpResponseCode)saveResult["Result"] == LookUpProvider.LookUpResponseCode.Success)
            {
                List<DataItem> debtTypes = LookUpProvider.GetDebtTypes();
                debtTypes.ToList().ForEach(t =>
                {
                    result.Add(t.Id, t.Name);
                });
            }
            return Json(result.ToArray<KeyValuePair<int, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Create, Update", ViewPage = "Manage Debt Types")]
        public JsonResult AddDebtTypesToCertificates(int debtTypeId)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            IDictionary<string, object> saveResult = LookUpProvider.AddDebtTypeToCertificates(debtTypeId);
            if ((LookUpProvider.LookUpResponseCode)saveResult["Result"] != LookUpProvider.LookUpResponseCode.Success)
            {
                result.Add("Error", saveResult["Result"].ToString());
                result.Add("ErrorMessage", saveResult["ErrorMessage"].ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }








[AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Filing Name Fee Types")]
        public ActionResult FilingNameFeeTypes()
        {
            List<DataItem> filingNameFeeTypes = LookUpProvider.GetFilingNameFeeTypes();
            filingNameFeeTypes.Insert(0, new DataItem() { Id = -1, Name = "- Select Filing Name Fee Type -", Value = "0" });
            ViewData["FilingNameFeeTypes"] = new SelectList(FilingNameFeeTypes, "Id", "Name");

            //Set view permissions for aspx
            MenuSecurityModel model = ControllerHelper.ConvertToMenuSecurityModel(UserProvider.GetViewPagePermission(((ClaimsIdentity)HttpContext.User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray()));

            return View(model);
        }

        [AuthorizeUserAttribute(Access = "Delete", ViewPage = "Manage Filing Name Fee Types")]
        public JsonResult DeleteFilingNameFeeType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, object> deleteResult = LookUpProvider.DeleteFilingNameFeeType(id);
            if ((LookUpProvider.LookUpResponseCode)deleteResult["Result"] != LookUpProvider.LookUpResponseCode.Success)
            {
                result.Add("ErrorMessage", deleteResult["ErrorMessage"].ToString());
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }


        [AuthorizeUserAttribute(Access = "Read", ViewPage = "Manage Filing Name Fee Types")]
        public JsonResult GetFilingNameFeeType(int id)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            FilingNameFeeType filingNameFeeType = LookUpProvider.GetFilingNameFeeType(id, null);
            if (debtType != null)
            {
                result.Add("Name", filingNameFeeType.Name);
              
            }
            return Json(result.ToArray<KeyValuePair<string, string>>(), JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUserAttribute(Access = "Create, Update", ViewPage = "Manage Filing Name Fee Types")]
        public JsonResult UpdateFilingNameType(int id, string name)
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            IDictionary<string, object> saveResult = LookUpProvider.SaveFilingNameFeeType(id, name);
            if ((LookUpProvider.LookUpResponseCode)saveResult["Result"] == LookUpProvider.LookUpResponseCode.Success)
            {
                List<DataItem> filingNameFeeTypes = LookUpProvider.GetFilingNameFeeTypes();
                filingNameFeeTypes.ToList().ForEach(t =>
                {
                    result.Add(t.Id, t.Name);
                });
            }
            return Json(result.ToArray<KeyValuePair<int, string>>(), JsonRequestBehavior.AllowGet);
        }

       