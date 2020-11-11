using Shareson.Model;

namespace Shareson.Repository
{
    public class ObservableCollectionControlRepository
    {
        public Data.FileInfoModel GetImageInfo(object obj)
        {
            var model = obj as Data.FileInfoModel;
            if(model != null)
            {
                return model;
            }
            else
            {
                model = new Data.FileInfoModel();
                return model;
            }
            
        }
    }
}
