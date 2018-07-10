using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace CaoJin.HNFinanceTool.Content
{
    public class ProjectEstimateViewModel:NotifyPropertyChanged
    {
        private string _projectName; //项目名称
        public string ProjectName 
        {
            get { return _projectName; }
            set { _projectName = value;OnPropertyChanged("ProjectName"); }
        }

        private string _projectCode;//项目编码
        public string ProjectCode
        {
            get { return _projectCode; }
            set { _projectCode = value;OnPropertyChanged("ProjectCode"); }
        }

        private string _individualProjectName;//单项工程名称
        public string IndividualProjectName
        {
            get { return _individualProjectName; }
            set { _individualProjectName = value;OnPropertyChanged("IndividualProjectName"); }
        }

        private string _individualProjectCode;//单项工程编码
        public string IndividualProjectCode
        {
            get { return _individualProjectCode; }
            set { _individualProjectCode = value;OnPropertyChanged("IndividualProjectCode"); }
        }

        private string _expanseCategory;//费用类别
        public string ExpanseCategory
        {
            get { return _expanseCategory; }
            set { _expanseCategory = value;OnPropertyChanged("ExpanseCategory"); }
        }

        private string _wbsCode;//wbs元素
        public string WBSCode
        {
            get { return _wbsCode; }
            set { _wbsCode = value;OnPropertyChanged("WBSCode"); }
        }

        private double EstimateNumber;//
    }
}
