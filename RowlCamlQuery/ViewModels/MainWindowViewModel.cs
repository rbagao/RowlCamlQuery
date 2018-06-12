using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RowlCamlQuery.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private string _site = "http://roel2013/sites/testingsite"; 
        public string Site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
                OnPropertyChanged(nameof(Site));
                OnPropertyChanged(nameof(CanExecute));
            }
        }

        private string _list = "TestLibrary";
        public string List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged(nameof(List));
                OnPropertyChanged(nameof(CanExecute));
            }
        }
        
        private string _contentTypeName = "Email";
        public string ContentTypeName
        {
            get
            {
                return _contentTypeName;
            }
            set
            {
                _contentTypeName = value;
                OnPropertyChanged(nameof(ContentTypeName));
                OnPropertyChanged(nameof(CanExecute));
            }
        }

        private string _query = "<View Scope='RecursiveAll'><Query><Where><And><Geq><FieldRef Name='ID' /><Value Type='Counter'>15</Value></Geq><And><Leq><FieldRef Name='ID' /><Value Type='Counter'>18</Value></Leq><Eq><FieldRef Name='DocIcon' /><Value Type='Computed'>msg</Value></Eq></And></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query></View>";
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
                OnPropertyChanged(nameof(CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return (!string.IsNullOrEmpty(this.Site) && !string.IsNullOrEmpty(this.List) && !string.IsNullOrEmpty(this.Query));
            }
        }

        private DataTable _results;
        public DataTable Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged(nameof(Results));
            }
        }

        // OK
        private RelayCommand _executeCommand;
        public ICommand ExecuteCommand
        {
            get
            {
                if (_executeCommand == null)
                {
                    this._executeCommand = new RelayCommand(x => GetListColumns());
                }
                return this._executeCommand;
            }
        }

        private async void GetListColumns()
        {
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        using (ClientContext context = new ClientContext(Site))
                        {
                            var list = context.Web.Lists.GetByTitle(List);
                            var query = new CamlQuery() { ViewXml = Query };
                            var items = list.GetItems(query);
                            context.Load(items);
                            context.ExecuteQuery();

                            DataTable dt = new DataTable();
                            Results = null;

                            if (items != null && items.Count() > 0)
                            {
                                foreach (var field in items[0].FieldValues.Keys)
                                {
                                    dt.Columns.Add(field);
                                }
                                foreach (var item in items)
                                {
                                    DataRow dr = dt.NewRow();

                                    foreach (var obj in item.FieldValues)
                                    {
                                        if (obj.Value != null)
                                        {
                                            string type = obj.Value.GetType().FullName;

                                            if (type == "Microsoft.SharePoint.Client.FieldLookupValue")
                                            {
                                                dr[obj.Key] = ((FieldLookupValue)obj.Value).LookupValue;
                                            }
                                            else if (type == "Microsoft.SharePoint.Client.FieldUserValue")
                                            {
                                                dr[obj.Key] = ((FieldUserValue)obj.Value).LookupValue;
                                            }
                                            else
                                            {
                                                dr[obj.Key] = obj.Value;
                                            }
                                        }
                                        else
                                        {
                                            dr[obj.Key] = null;
                                        }
                                    }

                                    dt.Rows.Add(dr);
                                }
                            }

                            Results = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void GetContentTypes()
        {
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        using (ClientContext context = new ClientContext(Site))
                        {
                            var contentTypeColl = context.Web.Lists.GetByTitle(List).ContentTypes;
                            context.Load(contentTypeColl, items => items.Include(item => item.Name));
                            context.ExecuteQuery();

                            DataTable dt = new DataTable();
                            Results = null;

                            if (contentTypeColl != null && contentTypeColl.Count() > 0)
                            {
                                dt.Columns.Add("Content Types");
                                foreach (var ct in contentTypeColl)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["Content Types"] = ct.Name;
                                    dt.Rows.Add(dr);
                                }
                            }
                            Results = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetContentTypeColumns()
        {
            try
            {
                //await System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        using (ClientContext context = new ClientContext(Site))
                        {
                            var listContentType = context.Web.Lists.GetByTitle(List).ContentTypes.GetByName(ContentTypeName);
                            FieldCollection fieldColl = listContentType.Fields;
                            context.Load(fieldColl);
                            context.ExecuteQuery();

                            DataTable dt = new DataTable();
                            Results = null;
                            if (fieldColl != null && fieldColl.Count() > 0)
                            {
                                dt.Columns.Add("Content Type Columns");
                                foreach (var field in fieldColl)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["Content Type Columns"] = field.Title;
                                    dt.Rows.Add(dr);
                                }
                            }
                            Results = dt;

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

    }

    public static class ContentTypeExtensions
    {
        public static ContentType GetByName(this ContentTypeCollection cts, string name)
        {
            var ctx = cts.Context;
            ctx.Load(cts);
            ctx.ExecuteQuery();
            return Enumerable.FirstOrDefault(cts, ct => ct.Name == name);
        }
    }

}
