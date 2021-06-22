using App.Scripts.Gameplay.Coins;
using UnityEditor;
using UnityEngine;

namespace Alex.Editor
{
    [CustomEditor(typeof(CoinsTable))]
    public class CoinsTableEditor : EditorWindow
    {
        private static CoinsTableEditor _window;
        private CoinsTable _coinsTableDatabase;

        private const int Offset = 75;
        private const int Spacing = 51;
        private const int SizeGrid = 50;

        [MenuItem("Game/Coins table")]
        private static void ShowWindow()
        {
            _window = (CoinsTableEditor)EditorWindow.GetWindow(typeof(CoinsTableEditor));
            _window.titleContent = new GUIContent ("Elements table");
            _window.Show();
        }

        private void OnGUI()
        {
            if (!CheckPossibleDraw())
            {
                return;
            }

            DrawGrid(Spacing, Color.black, SizeGrid * (_coinsTableDatabase.Elements.Count - 1), Offset);
            DrawIcons(SizeGrid, Offset);
            ShowElementValues(SizeGrid, Offset);
            
            ShowSaveButton();
            ShowLoadButton();
        }

        private bool CheckPossibleDraw()
        {
            if (Application.isPlaying)
            {
                CenterMessage("Elements table editor is not available in play mode.");
                return false;
            }
            
            _coinsTableDatabase = CoinsTable.Instance;
            if (CoinsTable.Instance == null)
            {
                CenterMessage("Elements table doesn't exist!");
                return false;
            }
            
            return true;
        }
        
        void CenterMessage(string message)
        {
            var text = new GUIContent(message);
            var textSize = GUI.skin.label.CalcSize(text);
            EditorGUI.LabelField(new Rect((_window.position.size - textSize) * 0.5f, textSize), text);
        }
        
        private void DrawGrid(float spacing, Color color, int size, int leftOffset)
        {
            float w = size;
            float h = size;
        
            int nx = Mathf.CeilToInt(w / spacing);
            int ny = Mathf.CeilToInt(h / spacing);

            var off = new Vector2(leftOffset + spacing, leftOffset + spacing);

            Handles.BeginGUI();
            Handles.color = color;

            for (int i = 0; i <= nx; i++) {
                var s = off + new Vector2(spacing * i, -spacing    );
                var e = off + new Vector2(spacing * i,  spacing + h);
                Handles.DrawLine(s, e);
            }
            for (int i = 0; i <= ny; i++) {
                var s = off + new Vector2(-spacing    , spacing * i);
                var e = off + new Vector2( spacing + w, spacing * i);
                Handles.DrawLine(s, e);
            }

            Handles.EndGUI();
        }

        private void DrawIcons(int sizeIcon, int offset)
        {
            //Line
            for (int i = 0; i < _coinsTableDatabase.Elements.Count; i++)
            {
                GUI.Label(new Rect(offset + ((sizeIcon + 1) * (i + 1)), offset - 15, sizeIcon, 15), 
                    new GUIContent(_coinsTableDatabase.Elements[i].Name));
                if (_coinsTableDatabase.Elements[i].Icon.texture != null)
                {
                    GUI.DrawTexture(new Rect(offset + ((sizeIcon + 1) * (i + 1)), offset, sizeIcon, sizeIcon), 
                        _coinsTableDatabase.Elements[i].Icon.texture);
                }
            }
            
            //Row
            for (int i = 0; i < _coinsTableDatabase.Elements.Count; i++)
            {
                if (_coinsTableDatabase.Elements[i].Icon.texture != null)
                {
                    GUI.DrawTexture(new Rect(offset, offset + ((sizeIcon + 1) * (i + 1)), sizeIcon, sizeIcon), 
                        _coinsTableDatabase.Elements[i].Icon.texture);
                }
            }
        }

        private void ShowElementValues(int sizeGrid, int offset)
        {
            for (int i = 0; i < _coinsTableDatabase.Elements.Count; i++)
            {
                for (int j = 0; j < _coinsTableDatabase.Elements.Count; j++)
                {
                    if (!_coinsTableDatabase.BonusElement[_coinsTableDatabase.Elements[i]]
                        .ContainsKey(_coinsTableDatabase.Elements[j]))
                    {
                        _coinsTableDatabase.BonusElement[_coinsTableDatabase.Elements[i]].Add(_coinsTableDatabase.Elements[j], 0);
                    }

                    _coinsTableDatabase.BonusElement[_coinsTableDatabase.Elements[i]][_coinsTableDatabase.Elements[j]] =
                        EditorGUI.FloatField(new Rect(offset + (int)(sizeGrid/10) + ((sizeGrid + 1) * (j + 1)),
                                offset + (int)(sizeGrid/4) + ((sizeGrid + 1) * (i + 1)),(int)(sizeGrid/1.2f),(int)(sizeGrid/2)),
                            _coinsTableDatabase.BonusElement[_coinsTableDatabase.Elements[i]][_coinsTableDatabase.Elements[j]]);
                }
            }
        }

        private void ShowSaveButton()
        {
            if (GUILayout.Button("Save"))
            {          
                _coinsTableDatabase.SaveData();
                
                EditorUtility.SetDirty(_coinsTableDatabase);
                AssetDatabase.SaveAssets();
            }
        }
        
        private void ShowLoadButton()
        {
            if (GUILayout.Button("Load"))
            {          
                _coinsTableDatabase.LoadData();
            }
        }

    }
}