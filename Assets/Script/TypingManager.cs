using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Question
{
    public string japanese;
    public string roman;
}

public class TypingManager : MonoBehaviour
{
    [SerializeField] private Question[] questions;
    [SerializeField] private TextMeshProUGUI textJapanese; // �����ɓ��{��\����TextMeshPro���A�^�b�`����B
    [SerializeField] private TextMeshProUGUI textRoman; // �����Ƀ��[�}���\����TextMeshPro���A�^�b�`����B
    public TMP_Text successCountText; // �\�����邽�߂�TextMeshPro�e�L�X�g�I�u�W�F�N�g
    public TMP_Text failureCountText;

    private readonly List<char> roman = new List<char>();

    private int romanIndex;
    private int successCount = 0; // �����������̃J�E���g
    private int failureCount = 0;

    private bool isWindows;
    private bool isMac;

    private bool acceptingInput = true; // ���͂��󂯕t���邩�ǂ����𐧌䂷��t���O
    private float inputDelayTime = 0.1f; // �~�X�^�C�v��̈ꎞ��~���ԁi�b�j

    private CountDownTimer timer;

    private void Start()
    {
        if (SystemInfo.operatingSystem.Contains("Windows"))
        {
            isWindows = true;
        }

        if (SystemInfo.operatingSystem.Contains("Mac"))
        {
            isMac = true;
        }

        timer = GameObject.FindObjectOfType<CountDownTimer>();

        InitializeQuestion();
    }

    private void Update()
    {
        float timeLeft = timer.GetTimeLeft();

        if (timeLeft <= 0)
        {
            StopAcceptingInput();
            SceneManager.LoadScene("ResultScene");
        }
    }

    private void OnGUI()
    {
        if (!acceptingInput)
        {
            return; // ���͂��󂯕t���Ȃ��ꍇ�͏��������ɖ߂�
        }

        if (Event.current.type == EventType.KeyDown)
        {
            switch (InputKey(GetCharFromKeyCode(Event.current.keyCode)))
            {
                case 1: // �����^�C�v��
                    romanIndex++;
                    if (roman[romanIndex] == '@') // �u@�v���^�C�s���O�̏I���̔���ƂȂ�B
                    {
                        InitializeQuestion();
                    }
                    else
                    {
                        textRoman.text = GenerateTextRoman();
                        OnSuccessTyping(); // �������̏������Ăяo��
                    }
                    break;
                case 2: // �~�X�^�C�v��
                    OnFailureTyping();
                    break;

            }
        }
    }

    void InitializeQuestion()
    {
        Question question = questions[UnityEngine.Random.Range(0, questions.Length)];

        roman.Clear();

        romanIndex = 0;

        char[] characters = question.roman.ToCharArray();

        foreach (char character in characters)
        {
            roman.Add(character);
        }

        roman.Add('@');

        textJapanese.text = question.japanese;
        textRoman.text = GenerateTextRoman();
    }

    private void OnSuccessTyping()
    {
        successCount++; // �����������𑝂₷
        UpdateSuccessCountText(); // �����������̕\�����X�V����
    }

    private void OnFailureTyping()
    {
        failureCount++;
        UpdateFailureCountText();
        acceptingInput = false; // �ꎞ��~���J�n
        StartCoroutine(ResumeInputAfterDelay(inputDelayTime)); // ��莞�Ԍ�ɓ��͂��ĊJ����
    }

    private IEnumerator ResumeInputAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        acceptingInput = true; // ���͂��ĊJ
    }

    private void UpdateSuccessCountText()
    {
        successCountText.text = "Success: " + successCount.ToString(); // �e�L�X�g���X�V����
    }

    private void UpdateFailureCountText()
    {
        failureCountText.text = "Failure: " + failureCount.ToString(); // �e�L�X�g���X�V����
    }

    public void StopAcceptingInput()
    {
        acceptingInput = false;
    }

    string GenerateTextRoman()
    {
        string text = "<style=typed>";
        for (int i = 0; i < roman.Count; i++)
        {
            if (roman[i] == '@')
            {
                break;
            }

            if (i == romanIndex)
            {
                text += "</style><style=untyped>";
            }

            text += roman[i];
        }

        text += "</style>";

        return text;
    }

    int InputKey(char inputChar)
    {
        char prevChar3 = romanIndex >= 3 ? roman[romanIndex - 3] : '\0';
        char prevChar2 = romanIndex >= 2 ? roman[romanIndex - 2] : '\0';
        char prevChar = romanIndex >= 1 ? roman[romanIndex - 1] : '\0';
        char currentChar = roman[romanIndex];
        char nextChar = roman[romanIndex + 1];
        char nextChar2 = nextChar == '@' ? '@' : roman[romanIndex + 2];

        if (inputChar == '\0')
        {
            return 0;
        }

        if (inputChar == currentChar)
        {
            return 1;
        }

        //�u���v�̏_��ȓ��́iWindows�̂݁j
        if (isWindows && inputChar == 'y' && currentChar == 'i' &&
            (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' ||
             prevChar == 'o'))
        {
            roman.Insert(romanIndex, 'y');
            return 1;
        }

        if (isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'n' &&
            prevChar3 != 'n')
        {
            roman.Insert(romanIndex, 'y');
            return 1;
        }

        if (isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'x')
        {
            roman.Insert(romanIndex, 'y');
            return 1;
        }

        //�u���v�̏_��ȓ��́i�uwhu�v��Windows�̂݁j
        if (inputChar == 'w' && currentChar == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' ||
                                                       prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
        {
            roman.Insert(romanIndex, 'w');
            return 1;
        }

        if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
        {
            roman.Insert(romanIndex, 'w');
            return 1;
        }

        if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'x')
        {
            roman.Insert(romanIndex, 'w');
            return 1;
        }

        if (isWindows && inputChar == 'h' && prevChar2 != 't' && prevChar2 != 'd' && prevChar == 'w' &&
            currentChar == 'u')
        {
            roman.Insert(romanIndex, 'h');
            return 1;
        }

        //�u���v�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
        if (isWindows && inputChar == 'c' && prevChar != 'k' &&
            currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
        {
            roman[romanIndex] = 'c';
            return 1;
        }

        //�u���v�̏_��ȓ��́iWindows�̂݁j
        if (isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
        {
            roman[romanIndex] = 'q';
            return 1;
        }

        //�u���v�̏_��ȓ���
        if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
        {
            roman.Insert(romanIndex, 'h');
            return 1;
        }

        //�u���v�̏_��ȓ���
        if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
        {
            roman[romanIndex] = 'j';
            return 1;
        }

        //�u����v�u����v�u�����v�u����v�̏_��ȓ���
        if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
        {
            roman[romanIndex] = 'h';
            return 1;
        }

        //�u����v�u����v�u�����v�u����v�̏_��ȓ���
        if (inputChar == 'z' && prevChar != 'j' && currentChar == 'j' &&
            (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            roman[romanIndex] = 'z';
            roman.Insert(romanIndex + 1, 'y');
            return 1;
        }

        if (inputChar == 'y' && prevChar == 'j' &&
            (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o'))
        {
            roman.Insert(romanIndex, 'y');
            return 1;
        }

        //�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
        if (isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
            (nextChar == 'i' || nextChar == 'e'))
        {
            roman[romanIndex] = 'c';
            return 1;
        }

        //�u���v�̏_��ȓ���
        if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
        {
            roman[romanIndex] = 'c';
            roman.Insert(romanIndex + 1, 'h');
            return 1;
        }

        //�u����v�u����v�u�����v�u����v�̏_��ȓ���
        if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
        {
            roman[romanIndex] = 'c';
            return 1;
        }

        //�ucya�v=>�ucha�v
        if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
        {
            roman[romanIndex] = 'h';
            return 1;
        }

        //�u�v�̏_��ȓ���
        if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
        {
            roman.Insert(romanIndex, 's');
            return 1;
        }

        //�u���v�u���v�u���v�u���v�̏_��ȓ���
        if (inputChar == 'u' && prevChar == 't' && currentChar == 's' &&
            (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
            roman[romanIndex] = 'u';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        if (inputChar == 'u' && prevChar2 == 't' && prevChar == 's' &&
            (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
        {
            roman.Insert(romanIndex, 'u');
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        //�u�Ă��v�̏_��ȓ���
        if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
        {
            roman[romanIndex] = 'e';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        //�u�ł��v�̏_��ȓ���
        if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
        {
            roman[romanIndex] = 'e';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        //�u�ł�v�̏_��ȓ���
        if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
        {
            roman[romanIndex] = 'e';
            roman.Insert(romanIndex + 1, 'x');
            roman.Insert(romanIndex + 2, 'y');
            return 1;
        }

        //�u�Ƃ��v�̏_��ȓ���
        if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
        {
            roman[romanIndex] = 'o';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }
        //�u�ǂ��v�̏_��ȓ���
        if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
        {
            roman[romanIndex] = 'o';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        //�u�Ӂv�̏_��ȓ���
        if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
        {
            roman[romanIndex] = 'f';
            return 1;
        }

        //�u�ӂ��v�u�ӂ��v�u�ӂ��v�u�ӂ��v�̏_��ȓ��́i�ꕔMac�̂݁j
        if (inputChar == 'w' && prevChar == 'f' &&
            (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
        {
            roman.Insert(romanIndex, 'w');
            return 1;
        }

        if (inputChar == 'y' && prevChar == 'f' && (currentChar == 'i' || currentChar == 'e'))
        {
            roman.Insert(romanIndex, 'y');
            return 1;
        }

        if (inputChar == 'h' && prevChar != 'f' && currentChar == 'f' &&
            (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
            if (isMac)
            {
                roman[romanIndex] = 'h';
                roman.Insert(romanIndex + 1, 'w');
            }
            else
            {
                roman[romanIndex] = 'h';
                roman.Insert(romanIndex + 1, 'u');
                roman.Insert(romanIndex + 2, 'x');
            }
            return 1;
        }

        if (inputChar == 'u' && prevChar == 'f' &&
            (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
        {
            roman.Insert(romanIndex, 'u');
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        if (isMac && inputChar == 'u' && prevChar == 'h' && currentChar == 'w' &&
            (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
            roman[romanIndex] = 'u';
            roman.Insert(romanIndex + 1, 'x');
            return 1;
        }

        //�u��v�̏_��ȓ��́i�un'�v�ɂ͖��Ή��j
        if (inputChar == 'n' && prevChar2 != 'n' && prevChar == 'n' && currentChar != 'a' && currentChar != 'i' &&
            currentChar != 'u' && currentChar != 'e' && currentChar != 'o' && currentChar != 'y')
        {
            roman.Insert(romanIndex, 'n');
            return 1;
        }

        if (inputChar == 'x' && prevChar != 'n' && currentChar == 'n' && nextChar != 'a' && nextChar != 'i' &&
            nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
        {
            if (nextChar == 'n')
            {
                roman[romanIndex] = 'x';
            }
            else
            {
                roman.Insert(romanIndex, 'x');
            }
            return 1;
        }

        //�u�����v�u�����v�u�����v�𕪉�����
        if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
        {
            roman[romanIndex] = 'u';
            roman[romanIndex] = 'x';
        }

        //�u����v�u�ɂ�v�Ȃǂ𕪉�����
        if (inputChar == 'i' && currentChar == 'y' &&
            (prevChar == 'k' || prevChar == 's' || prevChar == 't' || prevChar == 'n' || prevChar == 'h' ||
             prevChar == 'm' || prevChar == 'r' || prevChar == 'g' || prevChar == 'z' || prevChar == 'd' ||
             prevChar == 'b' || prevChar == 'p') &&
            (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            if (nextChar == 'e')
            {
                roman[romanIndex] = 'i';
                roman.Insert(romanIndex + 1, 'x');
            }
            else
            {
                roman.Insert(romanIndex, 'i');
                roman.Insert(romanIndex + 1, 'x');
            }
            return 1;
        }

        //�u����v�u����v�Ȃǂ𕪉�����
        if (inputChar == 'i' &&
            (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o') &&
            (prevChar2 == 's' || prevChar2 == 'c') && prevChar == 'h')
        {
            if (nextChar == 'e')
            {
                roman.Insert(romanIndex, 'i');
                roman.Insert(romanIndex + 1, 'x');
            }
            else
            {
                roman.Insert(romanIndex, 'i');
                roman.Insert(romanIndex + 1, 'x');
                roman.Insert(romanIndex + 2, 'y');
            }
            return 1;
        }

        //�u����v���uc�v�ŕ�������iWindows����j
        if (isWindows && inputChar == 'c' && currentChar == 's' && prevChar != 's' && nextChar == 'y' &&
            (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
        {
            if (nextChar2 == 'e')
            {
                roman[romanIndex] = 'c';
                roman[romanIndex + 1] = 'i';
                roman.Insert(romanIndex + 1, 'x');
            }
            else
            {
                roman[romanIndex] = 'c';
                roman.Insert(romanIndex + 1, 'i');
                roman.Insert(romanIndex + 2, 'x');
            }
            return 1;
        }

        //�u���v�̏_��ȓ���
        if ((inputChar == 'x' || inputChar == 'l') &&
            (currentChar == 'k' && nextChar == 'k' || currentChar == 's' && nextChar == 's' ||
             currentChar == 't' && nextChar == 't' || currentChar == 'g' && nextChar == 'g' ||
             currentChar == 'z' && nextChar == 'z' || currentChar == 'j' && nextChar == 'j' ||
             currentChar == 'd' && nextChar == 'd' || currentChar == 'b' && nextChar == 'b' ||
             currentChar == 'p' && nextChar == 'p'))
        {
            roman[romanIndex] = inputChar;
            roman.Insert(romanIndex + 1, 't');
            roman.Insert(romanIndex + 2, 'u');
            return 1;
        }

        //�u�����v�u�����v�u�����v�̏_��ȓ��́iWindows����j
        if (isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
            (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
        {
            roman[romanIndex] = 'c';
            roman[romanIndex + 1] = 'c';
            return 1;
        }

        //�u�����v�̏_��ȓ��́iWindows����j
        if (isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
        {
            roman[romanIndex] = 'q';
            roman[romanIndex + 1] = 'q';
            return 1;
        }

        //�u�����v�u�����v�̏_��ȓ��́iWindows����j
        if (isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
            (nextChar2 == 'i' || nextChar2 == 'e'))
        {
            roman[romanIndex] = 'c';
            roman[romanIndex + 1] = 'c';
            return 1;
        }

        //�u������v�u������v�u�������v�u������v�̏_��ȓ���
        if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
        {
            roman[romanIndex] = 'c';
            roman[romanIndex + 1] = 'c';
            return 1;
        }

        //�u�����v�̏_��ȓ���
        if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
        {
            roman[romanIndex] = 'c';
            roman[romanIndex + 1] = 'c';
            roman.Insert(romanIndex + 2, 'h');
            return 1;
        }

        //�ul�v�Ɓux�v�̊��S�݊���
        if (inputChar == 'x' && currentChar == 'l')
        {
            roman[romanIndex] = 'x';
            return 1;
        }

        if (inputChar == 'l' && currentChar == 'x')
        {
            roman[romanIndex] = 'l';
            return 1;
        }

        return 2;
    }

    char GetCharFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return 'a';
            case KeyCode.B:
                return 'b';
            case KeyCode.C:
                return 'c';
            case KeyCode.D:
                return 'd';
            case KeyCode.E:
                return 'e';
            case KeyCode.F:
                return 'f';
            case KeyCode.G:
                return 'g';
            case KeyCode.H:
                return 'h';
            case KeyCode.I:
                return 'i';
            case KeyCode.J:
                return 'j';
            case KeyCode.K:
                return 'k';
            case KeyCode.L:
                return 'l';
            case KeyCode.M:
                return 'm';
            case KeyCode.N:
                return 'n';
            case KeyCode.O:
                return 'o';
            case KeyCode.P:
                return 'p';
            case KeyCode.Q:
                return 'q';
            case KeyCode.R:
                return 'r';
            case KeyCode.S:
                return 's';
            case KeyCode.T:
                return 't';
            case KeyCode.U:
                return 'u';
            case KeyCode.V:
                return 'v';
            case KeyCode.W:
                return 'w';
            case KeyCode.X:
                return 'x';
            case KeyCode.Y:
                return 'y';
            case KeyCode.Z:
                return 'z';
            case KeyCode.Alpha0:
                return '0';
            case KeyCode.Alpha1:
                return '1';
            case KeyCode.Alpha2:
                return '2';
            case KeyCode.Alpha3:
                return '3';
            case KeyCode.Alpha4:
                return '4';
            case KeyCode.Alpha5:
                return '5';
            case KeyCode.Alpha6:
                return '6';
            case KeyCode.Alpha7:
                return '7';
            case KeyCode.Alpha8:
                return '8';
            case KeyCode.Alpha9:
                return '9';
            case KeyCode.Minus:
                return '-';
            case KeyCode.Caret:
                return '^';
            case KeyCode.Backslash:
                return '\\';
            case KeyCode.At:
                return '@';
            case KeyCode.LeftBracket:
                return '[';
            case KeyCode.Semicolon:
                return ';';
            case KeyCode.Colon:
                return ':';
            case KeyCode.RightBracket:
                return ']';
            case KeyCode.Comma:
                return ',';
            case KeyCode.Period:
                return '.';
            case KeyCode.Slash:
                return '/';
            case KeyCode.Underscore:
                return '_';
            case KeyCode.Backspace:
                return '\b';
            case KeyCode.Return:
                return '\r';
            case KeyCode.Space:
                return ' ';
            default: //��L�ȊO�̃L�[�������ꂽ�ꍇ�́unull�����v��Ԃ��B
                return '\0';
        }
    }
}