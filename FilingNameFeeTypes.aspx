<%@ Page Title="" Language="C#" MasterPageFile="~/Mvc/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<ComplianceRepository.UI.Mvc.Models.MenuSecurityModel>" %>

    <asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
        <title>Compliance Repository</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">      
        </style>      
    </asp:Content>
    
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <% Html.RenderPartial("~/Mvc/Views/Shared/MainMenu.ascx", Model); %>
        <h2 style="margin-bottom:35px;">Manage Filing Name Fee Types</h2>    
        <div class="row">
            <div class="col-lg-12">
                <div id="errorAlert" class="alert alert-danger alert-dismissable" style="display:none;">
                    <button id="closeErrorAlert" type="button" class="close" data-hide="alert" aria-hidden="true">&times;</button>
                    <div style="font-weight:bold;">Error!</div>                
                    <div id="errorAlertMessage"></div>
                </div> 
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <label for="FilingNameFeeType">FilingNameFee Type</label><br />
                <%= Html.DropDownList("FilingNameFeeType", (ViewData["FilingNameFeeTypes"] as SelectList),
                                                    new Dictionary<string, object>()
                                                    {   
                                                        { "class", "form-control form-input"},
                                                        { "id", "FilingNameFeeType"},
                                                        { "name", "FilingNameFeeType"},
                                                        { "style", "width:300px;display:inline;"}
                                                    })%>
                <span id="ManageFilingNameFeeTypePanel" style="">      
                    <%if (Model.SecurityItems.Where(i => i.ViewPage == "Manage Filing Name Fee Types" && i.Create).Count() > 0)
                        {%>                         
                    <span id="AddFilingNameFeeType" data-id="add" class="glyphicon glyphicon-plus" data-toggle="tooltip" title="Add" style="margin-left:10px;margin-right:10px;cursor:pointer;"></span>
                    <%} %>
                    <%if (Model.SecurityItems.Where(i => i.ViewPage == "Manage Filing Name Fee Types" && i.Update).Count() > 0)
                        {%> 
                    <span id="EditFilingNameFeeType" data-id="edit" class="glyphicon glyphicon-pencil" data-toggle="tooltip" title="Edit" style="margin-right:10px;cursor:pointer;"></span>
                    <%} %>
                    <%if (Model.SecurityItems.Where(i => i.ViewPage == "Manage Filing Name Fee Types" && i.Delete).Count() > 0)
                        {%> 
                    <span id="DeleteFilingNameFeeType" class="glyphicon glyphicon-remove" data-toggle="tooltip" title="Delete" style="margin-right:10px;cursor:pointer;"></span>
                    <%} %>
                    
                </span>
            </div>        
        </div>
    
        <div class="modal" id="UpdateFilingNameFeeTypeForm" tabindex="-1" role="dialog" aria-labelledby="UpdateFilingNameFeeTypeHeader" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">                    
                        <h5 class="modal-title" id="UpdateFilingNameFeeTypeHeader">Filing Name Fee Type</h5>                
                    </div>            
                    <div class="modal-body">                                
                        <input type="hidden" id="UpdateFilingNameFeeTypeId" />        
                        <div class="row" style="margin-bottom:15px;">
                            <div class="col-md-8">
                                <label for="UpdateFilingNameFeeType">Filing Name Fee Type</label> 
                                <input class="form-control form-input" type="text" id="UpdateFilingNameFeeType" name="UpdateFilingNameFeeType" 
                                        rel="popover" data-placement="right" data-trigger="manual" autocomplete="off" />                                                            
                            </div>                      
                        </div>     
                                                
                    </div>
                    <div class="modal-footer">
                        <button id="btnCloseUpdateFilingNameFeeType" type="button" class="btn btn-default" data-dismiss="modal" style="width:80px;">Close</button>               
                        <%if (Model.SecurityItems.Where(i => i.ViewPage == "Manage FilingNameFee Types" && i.Update).Count() > 0)
                            {%>          
                        <button id="btnUpdateFilingNameFeeType" type="button" class="btn btn-primary" rel="popover" data-placement="top" data-trigger="manual" style="width:80px;">Update</button>   
                        <%} %>                     
                    </div>
                </div>
            </div>
        </div>
    
        <script type="text/javascript">
            $.getScript("/app/js/viewcode/ManageFilingNameFeeTypes.js")
        </script>      
    
    </asp:Content>