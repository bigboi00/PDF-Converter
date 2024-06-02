using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace PdfConverter.ViewModels
{
    public class RewardPageViewModel : INotifyPropertyChanged
    {

        private int _rewardPoints = 100;
        public int RewardPoints
        {
            get => _rewardPoints;
            set
            {
                if (_rewardPoints != value)
                {
                    _rewardPoints = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsEditEnabled));
                    OnPropertyChanged(nameof(IsSplitEnabled));
                    OnPropertyChanged(nameof(IsCompressEnabled));
                }
            }
        }

        public bool IsEditEnabled => RewardPoints >= 10;
        public bool IsSplitEnabled => RewardPoints >= 5;
        public bool IsCompressEnabled => RewardPoints >= 15;

        public bool EditActivated { get; set; }
        public bool CompressActivated { get; set; }
        public bool SplitActivated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
