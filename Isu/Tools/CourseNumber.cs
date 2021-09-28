using System;
using System.Linq.Expressions;

namespace Isu.Tools
{
    public class CourseNumber
    {
        private int _num;
        private int _maxNum;
        private int _minNum;
        public CourseNumber(int num, int minNum = 1, int maxNum = 9)
        {
            if (num < minNum || num > maxNum) throw new IsuException($"Курс должен быть от {minNum} до {maxNum}.");
            _minNum = minNum;
            _maxNum = maxNum;
            _num = num;
        }

        public static CourseNumber operator ++(CourseNumber cur)
        {
            if (cur._num == cur._maxNum) throw new IsuException("Ваша учёба закончена, идите работать дворником.");
            cur._num++;
            return cur;
        }

        public int Get_num()
        {
            return _num;
        }

        public void NewLimit(int newMin, int newMax)
        {
            _maxNum = newMax;
            _minNum = newMin;
        }
    }
}