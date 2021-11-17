using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ProgressData
    {
        public static ProgressData progressData = new ProgressData();

        // 到達済みのステージ
        public stageName stageClearAchievement = stageName.Stage1;

        // ステージの説明
        public IReadOnlyDictionary<stageName, string> stageDescriptions { get; private set; }

        private Dictionary<stageName, string> _stageDescriptions__Data { get; set; }

        // ステージの表示名
        public IReadOnlyDictionary<stageName, string> stageDisplayName { get; private set; }

        private Dictionary<stageName, string> _stageDisplayName__Data { get; set; }

        public enum stageName
        {
            _none,
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            LastStage,
        }

        public ProgressData()
        {
            _stageDescriptions__Data = new Dictionary<stageName, string>()
            {
                { stageName.Stage1, "ステージ1の説明" },
                { stageName.Stage2, "ステージ2の説明" },
                { stageName.Stage3, "ステージ3の説明" },
                { stageName.Stage4, "ステージ4の説明" },
                { stageName.LastStage, "ラストステージの説明" },
            };

            stageDescriptions = new ReadOnlyDictionary<stageName, string>(_stageDescriptions__Data);

            _stageDisplayName__Data = new Dictionary<stageName, string>()
            {
                { stageName.Stage1, "Stage1" },
                { stageName.Stage2, "Stage2" },
                { stageName.Stage3, "Stage3" },
                { stageName.Stage4, "Stage4" },
                { stageName.LastStage, "Boss" },
            };

            stageDisplayName = new ReadOnlyDictionary<stageName, string>(_stageDisplayName__Data);
        }
    }
}
