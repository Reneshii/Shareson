using Shareson.Model;

namespace Shareson.Repository
{
    public class ObservableCollectionControlRepository
    {
        public FileInfoModel GetImageInfo(object obj)
        {
            var model = obj as FileInfoModel;
            if(model != null)
            {
                return model;
            }
            else
            {
                model = new FileInfoModel();
                return model;
            }
            
        }
    }
}
