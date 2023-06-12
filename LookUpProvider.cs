public static List<DataItem> GetAgencyTypes(bool getLicenses, int? industryType)
        {
            ComplianceRepositoryEntities db = new ComplianceRepositoryEntities();
            List<DataItem> query = new List<DataItem>();
            if (industryType.HasValue)
            {
                query = (from a in db.AgencyTypes where a.IndustryTypeId == industryType.Value orderby a.SortOrder select new DataItem { Id = a.Id, Name = a.Name, Value = a.AllCertificates.ToString(), Key = a.IndustryTypeId.Value }).ToList();
            }
            else
            {
                query = (from a in db.AgencyTypes orderby a.SortOrder select new DataItem { Id = a.Id, Name = a.Name, Value = a.AllCertificates.ToString(), Key = a.IndustryTypeId.Value }).ToList();
            }
            if (getLicenses)
            {
                foreach (DataItem item in query)
                {
                    List<DataItem> licenses = GetLicenseTypes(new List<int>() { item.Id });
                    List<string> agentLicenses = new List<string>();
                    foreach (DataItem license in licenses)
                    {
                        agentLicenses.Add(string.Format("{0},{1}", license.Id, license.Name));
                    }
                    item.Value = string.Join(";", agentLicenses.ToList());
                }
            }
            return query;
        }

public static Dictionary<string, object> DeleteAgencyType(int id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (ComplianceRepositoryEntities complianceDb = new ComplianceRepositoryEntities())
                {
                    AgencyType deleteAgencyType;
                    deleteAgencyType = GetAgencyType(id, complianceDb);
                    string agencyName = deleteAgencyType.Name;
                    complianceDb.AgencyTypes.Remove(deleteAgencyType);
                    complianceDb.SaveChanges();
                    DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Delete, "Agency Type", "Name", agencyName, string.Empty);
                    result.Add("Result", LookUpResponseCode.Success);
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.GetBaseException().Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    result.Add("Result", LookUpResponseCode.ExistingRecordsDeleteError);
                    result.Add("ErrorMessage", "This Agency Type is associated with other Certificates and/or Licenses and cannot be deleted.");
                }
                else
                {
                    result.Add("Result", LookUpResponseCode.UnspecifiedError);
                    result.Add("ErrorMessage", ex.GetBaseException().Message);
                }
            }
            catch (Exception ex)
            {
                result.Add("Result", LookUpResponseCode.UnspecifiedError);
                result.Add("ErrorMessage", ex.Message);
            }
            return result;
        }

public static AgencyType GetAgencyType(int id, ComplianceRepositoryEntities complianceDb)
        {
            return GetAgencyType(id, false, complianceDb);
        }

        public static AgencyType GetAgencyType(int id, bool getLicenseTypes, ComplianceRepositoryEntities complianceDb)
        {
            if (complianceDb == null)
            {
                complianceDb = new ComplianceRepositoryEntities();
            }
            if (getLicenseTypes)
            {
                return (from a in complianceDb.AgencyTypes.Include("LicenseDebtTypes")
                        where a.Id == id
                        select a).SingleOrDefault();
            }
            else
            {
                return (from a in complianceDb.AgencyTypes
                        where a.Id == id
                        select a).SingleOrDefault();
            }

public static Dictionary<string, object> SaveAgencyType(int id, string name, bool allCertificates, int industryTypeId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (ComplianceRepositoryEntities complianceDb = new ComplianceRepositoryEntities())
                {

                    AgencyType saveAgencyType;
                    if (id <= 0)
                    {
                        saveAgencyType = new AgencyType();
                        DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Insert, "Agency Type", "Name", string.Empty, name);
                        DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Insert, "Agency Type", "AllCertificates", string.Empty, allCertificates.ToString());
                        DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Insert, "Agency Type", "Industry Type", string.Empty, GetIndustryType(industryTypeId).Name);

                    }
                    else
                    {
                        saveAgencyType = GetAgencyType(id, complianceDb);
                        if (saveAgencyType.Name != name)
                        {
                            DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Update, "Agency Type", "Name", saveAgencyType.Name, name);
                        }
                        if (saveAgencyType.AllCertificates != allCertificates)
                        {
                            DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Update, "Agency Type", "AllCertificates", saveAgencyType.AllCertificates.ToString(), allCertificates.ToString());
                        }
                        if (saveAgencyType.IndustryTypeId != industryTypeId)
                        {
                            DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Update, "Agency Type", "Industry Type", saveAgencyType.IndustryType.Name, GetIndustryType(industryTypeId).Name);
                        }
                    }

                    saveAgencyType.Name = name;
                    saveAgencyType.SortOrder = GetAgencyTypes(false, null).Count + 1;
                    saveAgencyType.AllCertificates = allCertificates;
                    saveAgencyType.IndustryTypeId = industryTypeId;

                    if (id <= 0)
                    {
                        complianceDb.AgencyTypes.Add(saveAgencyType);
                    }

                    complianceDb.SaveChanges();
                    result.Add("Result", LookUpResponseCode.Success);
                    result.Add("AgencyType", saveAgencyType);
                }
            }
            catch (Exception ex)
            {
                result.Add("Result", LookUpResponseCode.UnspecifiedError);
                result.Add("ErrorMessage", ex.Message);
            }
            return result;
        }


public static List<DataItem> GetDebtTypes()
        {
            ComplianceRepositoryEntities db = new ComplianceRepositoryEntities();
            List<DataItem> query = (from d in db.DebtTypes orderby d.Name select new DataItem { Id = d.Id, Name = d.Name, Value = d.AllCertificates.ToString() }).ToList();
            return query;
        }

        public static DebtType GetDebtType(int id, ComplianceRepositoryEntities complianceDb)
        {
            return GetDebtType(id, false, complianceDb);
        }

        public static DebtType GetDebtType(int id, bool getLicenseTypes, ComplianceRepositoryEntities complianceDb)
        {
            if (complianceDb == null)
            {
                complianceDb = new ComplianceRepositoryEntities();
            }
            if (getLicenseTypes)
            {
                return (from d in complianceDb.DebtTypes.Include("LicenseDebtTypes")
                        where d.Id == id
                        select d).SingleOrDefault();
            }
            else
            {
                return (from d in complianceDb.DebtTypes
                        where d.Id == id
                        select d).SingleOrDefault();
            }
        }

        public static Dictionary<string, object> SaveDebtType(int id, string name, bool allCertificates)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (ComplianceRepositoryEntities complianceDb = new ComplianceRepositoryEntities())
                {
                    DebtType saveDebtType;
                    if (id <= 0)
                    {
                        saveDebtType = new DebtType();
                        DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Insert, "Debt Type", "Name", string.Empty, name);
                        DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Insert, "Debt Type", "AllCertificates", string.Empty, allCertificates.ToString());
                    }
                    else
                    {
                        saveDebtType = GetDebtType(id, complianceDb);
                        if (saveDebtType.Name != name)
                        {
                            DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Update, "Debt Type", "Name", saveDebtType.Name, name);
                        }
                        if (saveDebtType.AllCertificates != allCertificates)
                        {
                            DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Update, "Debt Type", "AllCertificates", saveDebtType.AllCertificates.ToString(), allCertificates.ToString());
                        }
                    }

                    saveDebtType.Name = name;
                    saveDebtType.AllCertificates = allCertificates;

                    if (id <= 0)
                    {
                        complianceDb.DebtTypes.Add(saveDebtType);
                    }

                    complianceDb.SaveChanges();
                    result.Add("Result", LookUpResponseCode.Success);
                    result.Add("DebtType", saveDebtType);
                }
            }
            catch (Exception ex)
            {
                result.Add("Result", LookUpResponseCode.UnspecifiedError);
                result.Add("ErrorMessage", ex.Message);
            }
            return result;
        }

        public static Dictionary<string, object> DeleteDebtType(int id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                using (ComplianceRepositoryEntities complianceDb = new ComplianceRepositoryEntities())
                {
                    DebtType deleteDebtType;
                    deleteDebtType = GetDebtType(id, complianceDb);
                    string debtTypeName = deleteDebtType.Name;
                    complianceDb.DebtTypes.Remove(deleteDebtType);
                    complianceDb.SaveChanges();
                    DataAuditHelper.LogDataChange(DataAuditHelper.ActionCode.Delete, "Debt Type", "Name", debtTypeName, string.Empty);
                    result.Add("Result", LookUpResponseCode.Success);
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.GetBaseException().Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    result.Add("Result", LookUpResponseCode.ExistingRecordsDeleteError);
                    result.Add("ErrorMessage", "This Debt Type is associated with other Certificates and/or Licenses and cannot be deleted.");
                }
                else
                {
                    result.Add("Result", LookUpResponseCode.UnspecifiedError);
                    result.Add("ErrorMessage", ex.GetBaseException().Message);
                }
            }
            catch (Exception ex)
            {
                result.Add("Result", LookUpResponseCode.UnspecifiedError);
                result.Add("ErrorMessage", ex.Message);
            }
            return result;
        }