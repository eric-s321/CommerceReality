using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
//using UnityEngine.UI;

namespace Gamestrap
{
    [Serializable]
    public class GamestrapUI : EditorWindow
    {
        #region Editor variables
        private Font font;
        private Color normal;
        private Color highlighted;
        private Color pressed;
        private Color disabled;
        private Color detail;
        private bool showColors = false;
        private bool showSuggestions = false;
        private bool showSceneColors = false;

        private bool shadow;
        private Color shadowColor = new Color(0, 0, 0, 0.5f);
        private Vector2 shadowDistance = new Vector2(0, -2f);

        private bool gradient;
        private Color gradientTop = Color.white;
        private Color gradientBottom = new Color(0.45f, 0.45f, 0.45f);

        private bool _expanded;
        private Color selectedColor = GamestrapHelper.ColorRGBInt(31, 153, 200);
        private List<Color[]> colors;
        private List<Color> sceneColors;
        private PaletteType paletteType;
        private GUIStyle btnStyle, bgStyle, titleStyle;
        #endregion

        // Help variables
        private Vector2 scrollPos;
        private int recursiveLevel;

        [MenuItem("Window/Gamestrap UI Kit")]
        public static void ShowWindow()
        {
            GamestrapUI gs = (GamestrapUI)EditorWindow.GetWindow(typeof(GamestrapUI), false, "Gamestrap Kit");
            gs.minSize = new Vector2(295f, 305f);

        }

        void OnEnable()
        {
            //Load initila variables
            SetColors(GetColorDefault(GamestrapHelper.ColorRGBInt(141, 39, 137)));
            font = (Font)AssetDatabase.LoadAssetAtPath(GamestrapHelper.gamestrapRoute + "Fonts/Lato/Lato-Regular.ttf", typeof(Font));
            SetUpColors();
            sceneColors = new List<Color>();
        }

        void OnGUI()
        {
            bool lastShowSceneColors = showSceneColors;
            SetStyles();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            #region UI Colors
            GUILayout.Label("UI Colors", titleStyle);
            normal = EditorGUILayout.ColorField("Normal", normal);
            highlighted = EditorGUILayout.ColorField("Highlighted", highlighted);
            pressed = EditorGUILayout.ColorField("Pressed", pressed);
            disabled = EditorGUILayout.ColorField("Disabled", disabled);
            detail = EditorGUILayout.ColorField("Detail", detail);

            if (GUILayout.Button("Apply Colors to Selected UI"))
            {
                AssignColorsToSelection();
            }

            GUILayout.BeginHorizontal();
            showColors = GUILayout.Toggle(showColors, "Suggestions", "Button");
            showSuggestions = GUILayout.Toggle(showSuggestions, "Schemes", "Button");
            showSceneColors = GUILayout.Toggle(showSceneColors, "Scene Colors", "Button");
            // If the toggle was activated, refresh and search for the new colors in scene
            if (showSceneColors != lastShowSceneColors && showSceneColors)
            {
                SearchSceneColors();
            }
            GUILayout.EndHorizontal();

            Color defaultBg = GUI.backgroundColor;
            if (showSceneColors)
            {
                GUILayout.Label("Scene Colors", EditorStyles.boldLabel);
                GUI.backgroundColor = Color.black;
                GUILayout.BeginVertical(bgStyle);
                GUILayout.BeginHorizontal();
                int counter = 0;
                foreach (Color color in sceneColors)
                {
                    GUI.backgroundColor = color; // Sets the button color
                    if (GUILayout.Button("", btnStyle))
                    {
                        SetColors(GetColorDefault(color));
                        selectedColor = color;
                    }
                    counter++;
                    if (counter % 5 == 0)
                    {
                        // Start a new row each 5
                        GUILayout.EndHorizontal();
                        GUI.backgroundColor = Color.black;
                        GUILayout.BeginHorizontal();
                    }
                }
                GUILayout.EndHorizontal();
                GUI.backgroundColor = defaultBg; // Resets the color background

                GUILayout.EndVertical();
            }

            if (showSuggestions)
            {
                GUILayout.Label("Color Scheme Generator", EditorStyles.boldLabel);
                paletteType = (PaletteType)EditorGUILayout.EnumPopup("Scheme: ", paletteType);
                selectedColor = EditorGUILayout.ColorField("Base:", selectedColor);

                GUI.backgroundColor = Color.black;
                GUILayout.BeginVertical(bgStyle);
                GUI.backgroundColor = selectedColor; // Sets the button color
                if (GUILayout.Button("", btnStyle))
                {
                    SetColors(GetColorDefault(selectedColor));
                }
                Color[] paletteColors = GamestrapHelper.GetColorPalette(selectedColor, paletteType);
                GUILayout.BeginHorizontal();
                for (int i = 0; i < paletteColors.Length; i++)
                {
                    GUI.backgroundColor = paletteColors[i]; // Sets the button color
                    if (GUILayout.Button("", btnStyle))
                    {
                        SetColors(GetColorDefault(paletteColors[i]));
                    }
                }
                GUI.backgroundColor = defaultBg; // Resets the color background
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }

            if (showColors)
            {
                GUILayout.Label("Color Suggestions", EditorStyles.boldLabel);
                GUI.backgroundColor = Color.black;
                GUILayout.BeginVertical(bgStyle);
                GUILayout.BeginHorizontal();
                int counter = 0;
                foreach (Color[] color in colors)
                {
                    GUI.backgroundColor = color[0]; // Sets the button color
                    if (GUILayout.Button("", btnStyle) && color.Length >= 5)
                    {
                        SetColors(color);
                        selectedColor = color[0];
                    }
                    counter++;
                    if (counter % 5 == 0)
                    {
                        // Start a new row each 5
                        GUILayout.EndHorizontal();
                        GUI.backgroundColor = Color.black;
                        GUILayout.BeginHorizontal();
                    }
                }
                GUILayout.EndHorizontal();
                GUI.backgroundColor = defaultBg; // Resets the color background

                GUILayout.EndVertical();
            }

            #endregion

            #region Font
            GUILayout.Label("Font", titleStyle);
            GUILayout.BeginHorizontal();
            font = (Font)EditorGUILayout.ObjectField(font, typeof(Font), false);
            if (GUILayout.Button("Apply"))
            {
                AssignFontToSelection();
            }
            GUILayout.EndHorizontal();
            #endregion

            #region Effects
            GUILayout.Label("Effects", titleStyle);
            shadow = EditorGUILayout.ToggleLeft("Shadow", shadow);
            if (shadow)
            {
                shadowColor = EditorGUILayout.ColorField("Color", shadowColor);
                shadowDistance = EditorGUILayout.Vector2Field("Effect Distance", shadowDistance);
            }
            gradient = EditorGUILayout.ToggleLeft("Gradient", gradient);
            if (gradient)
            {
                gradientTop = EditorGUILayout.ColorField("Color Top", gradientTop);
                gradientBottom = EditorGUILayout.ColorField("Color Bottom", gradientBottom);
            }

            if (GUILayout.Button("Activate/Deactivate"))
            {
                ActivateEffects();
            }
            #endregion

            EditorGUILayout.EndScrollView();
        }

        #region Scene Color Methods
        private void SearchSceneColors()
        {
            sceneColors.Clear();
            GetSceneColors();
        }

        public void GetSceneColors()
        {
            foreach (var root in GamestrapHelper.GetSceneGameObjectRoots())
            {
                SearchColorsGameObject(root);
            }
        }

        private void SearchColorsGameObject(GameObject gameObject)
        {
            if (gameObject.GetComponent<UnityEngine.UI.Text>())
            {
                AddSceneColor(gameObject.GetComponent<UnityEngine.UI.Text>().color);
            }
            if (gameObject.GetComponent<UnityEngine.UI.Image>())
            {
                AddSceneColor(gameObject.GetComponent<UnityEngine.UI.Image>().color);
            }
            if (gameObject.GetComponent<UnityEngine.UI.Button>())
            {
                AddSceneColor(gameObject.GetComponent<UnityEngine.UI.Button>());
            }
            if (gameObject.GetComponent<UnityEngine.UI.Toggle>())
            {
                AddSceneColor(gameObject.GetComponent<UnityEngine.UI.Toggle>());
            }
            if (gameObject.GetComponent<UnityEngine.UI.Slider>())
            {
                AddSceneColor(gameObject.GetComponent<UnityEngine.UI.Slider>());
            }

            if (gameObject.transform.childCount > 0)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    SearchColorsGameObject(gameObject.transform.GetChild(i).gameObject);
                }
            }
        }

        private void AddSceneColor(Color color)
        {
            if (!sceneColors.Contains(color))
            {
                sceneColors.Add(color);
            }
        }

        private void AddSceneColor(UnityEngine.UI.Selectable selectable)
        {
            UnityEngine.UI.ColorBlock colorBlock = selectable.colors;
            AddSceneColor(colorBlock.normalColor);
            // Uncomment if you want to include additional colorBlock colors
            //AddSceneColor(colorBlock.highlightedColor);
            //AddSceneColor(colorBlock.pressedColor);
            //AddSceneColor(colorBlock.disabledColor);
        }
        #endregion

        #region Set Custom GUI Styles

        void SetStyles()
        {
            if (btnStyle == null)
            {
                SetBtnStyle();
            }
            if (bgStyle == null)
            {
                SetBgStyle();
            }
            if (titleStyle == null)
            {
                SetTitleStyle();
            }
        }

        void SetBtnStyle()
        {
            btnStyle = new GUIStyle(GUI.skin.button);
            Sprite bg = (Sprite)AssetDatabase.LoadAssetAtPath(GamestrapHelper.gamestrapRoute + "Editor/UI/Button Normal.psd", typeof(Sprite));
            Sprite bgClicked = (Sprite)AssetDatabase.LoadAssetAtPath(GamestrapHelper.gamestrapRoute + "Editor/UI/Button Pressed.psd", typeof(Sprite));
            btnStyle.margin = new RectOffset(0, 0, 0, 0);
            btnStyle.padding = new RectOffset(0, 0, 4, 4);
            btnStyle.normal.background = bg.texture;
            btnStyle.active.background = bgClicked.texture;
        }
        void SetBgStyle()
        {
            bgStyle = new GUIStyle(GUI.skin.box);
            bgStyle.margin = new RectOffset(4, 4, 0, 4);
            bgStyle.padding = new RectOffset(1, 1, 1, 2);
        }
        void SetTitleStyle()
        {
            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 15;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.LowerCenter;
            titleStyle.margin = new RectOffset(4, 4, 10, 0);
        }
        #endregion

        #region Assign Color Section
        /// <summary>
        /// Sets the color to the UI elements based on what types of components they have
        /// </summary>
        private void AssignColorsToSelection()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                recursiveLevel = 0;
                AssignColorsToSelection(go);
                // This resets the element so it updates the colors in the editor
                go.SetActive(false);
                go.SetActive(true);
            }
        }

        public void AssignColorsToSelection(GameObject gameObject)
        {
            recursiveLevel++;
            if (gameObject.GetComponent<UnityEngine.UI.Button>())
            {
                UnityEngine.UI.Button button = gameObject.GetComponent<UnityEngine.UI.Button>();
                SetColorBlock(button);
                SetDetailColor(gameObject);
                EditorUtility.SetDirty(button);
            }
            else if (gameObject.GetComponent<UnityEngine.UI.InputField>())
            {
                UnityEngine.UI.InputField input = gameObject.GetComponent<UnityEngine.UI.InputField>();
                SetColorBlock(input);
                input.selectionColor = highlighted;
                Undo.RecordObject(input.textComponent, "Change Text color");
                input.textComponent.color = pressed;
                Undo.RecordObject(input.placeholder, "Change Placeholder color");
                input.placeholder.color = highlighted;
                EditorUtility.SetDirty(input);
                EditorUtility.SetDirty(input.textComponent);
                EditorUtility.SetDirty(input.placeholder);
            }
            else if (gameObject.GetComponent<UnityEngine.UI.Scrollbar>())
            {
                UnityEngine.UI.Scrollbar sb = gameObject.GetComponent<UnityEngine.UI.Scrollbar>();
                SetColorBlock(sb);
                Undo.RecordObject(gameObject.GetComponent<UnityEngine.UI.Image>(), "Change Image color");
                gameObject.GetComponent<UnityEngine.UI.Image>().color = disabled;
                EditorUtility.SetDirty(sb);
                EditorUtility.SetDirty(gameObject.GetComponent<UnityEngine.UI.Image>());
            }
            else if (gameObject.GetComponent<UnityEngine.UI.Slider>())
            {
                UnityEngine.UI.Slider slider = gameObject.GetComponent<UnityEngine.UI.Slider>();
                SetColorBlock(slider);
                Undo.RecordObject(slider.fillRect.gameObject.GetComponent<UnityEngine.UI.Image>(), "Change Image color");
                slider.fillRect.gameObject.GetComponent<UnityEngine.UI.Image>().color = normal;
                SetTextColorRecursive(gameObject);
                EditorUtility.SetDirty(slider);
                EditorUtility.SetDirty(slider.fillRect.gameObject.GetComponent<UnityEngine.UI.Image>());
            }
            else if (gameObject.GetComponent<UnityEngine.UI.Toggle>())
            {
                UnityEngine.UI.Toggle toggle = gameObject.GetComponent<UnityEngine.UI.Toggle>();
                SetColorBlock(toggle);
                Undo.RecordObject(toggle.graphic, "Change Image color");
                toggle.graphic.color = normal;
                SetTextColorRecursive(gameObject);
                EditorUtility.SetDirty(toggle);
                EditorUtility.SetDirty(toggle.graphic);
            }
            else if (gameObject.GetComponent<UnityEngine.UI.Dropdown>())
            {
                UnityEngine.UI.Dropdown dropdown = gameObject.GetComponent<UnityEngine.UI.Dropdown>();
                SetColorBlock(dropdown);
                Undo.RecordObject(gameObject.GetComponent<UnityEngine.UI.Image>(), "Change Image color");
                gameObject.GetComponent<UnityEngine.UI.Image>().color = disabled;
                if (dropdown.captionText)
                {
                    dropdown.captionText.color = detail;   
                }
                if (dropdown.itemText)
                {
                    dropdown.itemText.color = detail;
                }
                SetColorRecursive(gameObject, typeof(UnityEngine.UI.Image), detail);
                SetColorRecursive(gameObject, typeof(UnityEngine.UI.ScrollRect), normal);
                SetColorRecursive(gameObject, typeof(UnityEngine.UI.Text), detail);
                SetColorRecursive(gameObject,typeof(UnityEngine.UI.Toggle), detail);
                SetColorRecursive(gameObject, typeof(UnityEngine.UI.Scrollbar), detail);
                EditorUtility.SetDirty(dropdown);
                EditorUtility.SetDirty(gameObject.GetComponent<UnityEngine.UI.Image>());
            }
            else if (gameObject.transform.childCount > 0) // Recursive search for components
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    AssignColorsToSelection(gameObject.transform.GetChild(i).gameObject);
                }
            }
            else if (recursiveLevel == 1)
            {
                if (gameObject.GetComponent<UnityEngine.UI.Image>())
                {
                    UnityEngine.UI.Image image = gameObject.GetComponent<UnityEngine.UI.Image>();
                    Undo.RecordObject(image, "Change color");
                    image.color = normal;
                    EditorUtility.SetDirty(image);
                }
                else if (gameObject.GetComponent<UnityEngine.UI.Text>())
                {
                    UnityEngine.UI.Text text = gameObject.GetComponent<UnityEngine.UI.Text>();
                    Undo.RecordObject(text, "Change color");
                    text.color = normal;
                    EditorUtility.SetDirty(text);
                }
            }
        }

        /// <summary>
        /// Sets all of the current variables to a ColorBlock and returns it
        /// </summary>
        /// <param name="cb">The color block from the UI element</param>
        /// <returns>The color block with the new values</returns>
        public void SetColorBlock(UnityEngine.UI.Selectable selectable)
        {
            UnityEngine.UI.Image img = selectable.GetComponent<UnityEngine.UI.Image>();
            Undo.RecordObject(selectable, "Change ColorBlock");
            if (selectable.transition == UnityEngine.UI.Selectable.Transition.ColorTint)
            {
                if (img)
                    img.color = Color.white;
                UnityEngine.UI.ColorBlock cb = selectable.colors;
                cb.normalColor = normal;
                cb.highlightedColor = highlighted;
                cb.pressedColor = pressed;
                cb.disabledColor = disabled;
                selectable.colors = cb;
            }
            else if (selectable.GetComponent<UnityEngine.UI.Image>())
            {
                img.color = normal;
            }
        }

        public void SetColors(Color[] color)
        {
            if (color.Length < 5)
            {
                Debug.LogError("Array too short, the color Array needs to be of length count 5");
                return;
            }
            normal = color[0];
            highlighted = color[1];
            pressed = color[2];
            disabled = color[3];
            detail = color[4];
        }

        /// <summary>
        /// Searches if the GameObject has children and if the children have components type Image or Text.
        /// If they do then it will assign the variable detail to the Color of the image or text.
        /// </summary>
        /// <param name="go">UI GameObject with children</param>
        private void SetDetailColor(GameObject go)
        {
            int children = go.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                if (child.GetComponent<UnityEngine.UI.Image>())
                {
                    Undo.RecordObject(child.GetComponent<UnityEngine.UI.Image>(), "Change Image color");
                    child.GetComponent<UnityEngine.UI.Image>().color = detail;
                    EditorUtility.SetDirty(child.GetComponent<UnityEngine.UI.Image>());
                }
                if (child.GetComponent<UnityEngine.UI.Text>())
                {
                    UnityEngine.UI.Text t = child.GetComponent<UnityEngine.UI.Text>();
                    Undo.RecordObject(t, "Change Text color");
                    t.color = detail;
                    EditorUtility.SetDirty(t);
                }
            }
        }

        /// <summary>
        /// Looks recursively for component Text in the GameObjects children 
        /// and also changes the color to the Detail variable if it finds any.
        /// </summary>
        /// <param name="go">UI GameObject with children</param>
        private void SetTextColorRecursive(GameObject go)
        {
            int children = go.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                if (child.GetComponent<UnityEngine.UI.Text>())
                {
                    UnityEngine.UI.Text t = child.GetComponent<UnityEngine.UI.Text>();
                    Undo.RecordObject(t, "Change Text color");
                    t.color = normal;
                    EditorUtility.SetDirty(t);
                }
                SetTextColorRecursive(child);
            }
        }

        private void SetColorRecursive(GameObject go, Type type, Color color)
        {
            int children = go.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                if (child.GetComponent(type))
                {
                    UnityEngine.UI.Selectable s = child.GetComponent<UnityEngine.UI.Selectable>();
                    UnityEngine.UI.Graphic g = child.GetComponent<UnityEngine.UI.Graphic>();
                    if (s)
                    {
                        Undo.RecordObject(s, "Change color");
                        SetColorBlock(s);
                        EditorUtility.SetDirty(s);
                    } else if (g)
                    {
                        Undo.RecordObject(g, "Change color");
                        g.color = color;
                        EditorUtility.SetDirty(g);
                    }
                }
                SetColorRecursive(child, type, color);
            }
        }
        #endregion

        #region Set Font
        private void AssignFontToSelection()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                AssignFontToSelection(go);
                // This resets the element so it updates the colors in the editor
                go.SetActive(false);
                go.SetActive(true);
            }
        }

        private void AssignFontToSelection(GameObject gameObject)
        {
            if (gameObject.GetComponent<UnityEngine.UI.Text>())
            {
                UnityEngine.UI.Text text = gameObject.GetComponent<UnityEngine.UI.Text>();
                if (font)
                {
                    Undo.RecordObject(text, "Change Font");
                    text.font = font;
                    EditorUtility.SetDirty(text);
                }
            }
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                AssignFontToSelection(gameObject.transform.GetChild(i).gameObject);
            }
        }

        #endregion

        #region Activate/Deactivate Effects
        /// <summary>
        /// Sets the color to the UI elements based on what types of components they have
        /// </summary>
        private void ActivateEffects()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                ActivateEffects(go);
                // This resets the element so it updates the colors in the editor
                go.SetActive(false);
                go.SetActive(true);
            }
        }

        public void ActivateEffects(GameObject gameObject)
        {
            if (gameObject.GetComponent<UnityEngine.UI.Image>() || gameObject.GetComponent<UnityEngine.UI.Text>())
            {
                if (shadow)
                {
                    ShadowEffect shadowEffect = gameObject.GetComponent<ShadowEffect>();
                    if (!shadowEffect)
                    {
                        shadowEffect = Undo.AddComponent<ShadowEffect>(gameObject);
                    }
                    shadowEffect.effectDistance = shadowDistance;
                    shadowEffect.effectColor = shadowColor;
                    EditorUtility.SetDirty(gameObject);
                }
                else if (gameObject.GetComponent<ShadowEffect>())
                {
                    Undo.DestroyObjectImmediate(gameObject.GetComponent<ShadowEffect>());
                }
            }

            if (gameObject.GetComponent<UnityEngine.UI.Image>())
            {
                if (gradient)
                {
                    GradientEffect gradientEffect = gameObject.GetComponent<GradientEffect>();
                    if (!gradientEffect)
                    {
                        gradientEffect = Undo.AddComponent<GradientEffect>(gameObject);
                    }
                    gradientEffect.top = gradientTop;
                    gradientEffect.bottom = gradientBottom;
                    EditorUtility.SetDirty(gameObject);
                }
                else if (gameObject.GetComponent<GradientEffect>())
                {
                    Undo.DestroyObjectImmediate(gameObject.GetComponent<GradientEffect>());
                }
            }

            if (gameObject.transform.childCount > 0) // Recursive search for components
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    ActivateEffects(gameObject.transform.GetChild(i).gameObject);
                }
            }
        }
        #endregion

        #region Color suggestion methods
        /// <summary>
        /// Adds all of the color arrays the editor window will suggest.
        /// </summary>
        public void SetUpColors()
        {
            colors = new List<Color[]>();
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(74, 37, 68)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(206, 20, 90)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(141, 39, 137)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(37, 82, 102)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(41, 165, 220)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(126, 209, 232)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(54, 148, 104)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(134, 192, 63)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(211, 218, 33)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(255, 204, 0)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(255, 153, 0)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(255, 173, 67)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(242, 110, 37)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(255, 102, 0)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(239, 106, 65)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(230, 36, 45)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(137, 24, 16)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(239, 101, 101)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(134, 98, 57)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(91, 54, 21)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(192, 150, 109)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(200, 200, 200)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(128, 128, 128)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(51, 51, 51)));
            colors.Add(GetColorDefault(GamestrapHelper.ColorRGBInt(21, 21, 21)));
        }

        /// <summary>
        /// Helper methods to create a color array
        /// </summary>
        /// <param name="baseColor">Base color of what the UI will look like</param>
        /// <returns></returns>
        public static Color[] GetColorDefault(Color baseColor)
        {
            Color highlighted = Color.Lerp(baseColor, Color.white, 0.3f);
            Color pressed = Color.Lerp(baseColor, Color.black, 0.6f);
            Color disabled = GamestrapHelper.ColorRGBInt(224, 224, 224);
            Color detail = Color.white;
            return new Color[] { baseColor, highlighted, pressed, disabled, detail };
        }
        #endregion
    }
}