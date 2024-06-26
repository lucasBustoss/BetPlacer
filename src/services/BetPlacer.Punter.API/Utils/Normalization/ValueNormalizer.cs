namespace BetPlacer.Punter.API.Utils.Normalization
{
    public class ValueNormalizer
    {
        private Queue<double> _queue;

        public ValueNormalizer()
        {
            _queue = new Queue<double>(5);
        }

        public double[] ToArray()
        {
            return _queue.ToArray();
        }

        public double Normalize(double value, int index)
        {
            Enqueue(value);

            if (index < 4)
                return 0;

            double maxValue = GetMaxItem();
            double minValue = GetMinItem();

            double dividend = value - minValue;
            double divider = maxValue - minValue;

            if (divider == 0)
                return 0;

            double result = dividend / divider;
            return result;
        }

        public void Enqueue(double item)
        {
            if (_queue.Count >= 5)
                _queue.Dequeue();

            _queue.Enqueue(item);
        }

        private double GetMaxItem()
        {
            return _queue.Max();
        }

        private double GetMinItem()
        {
            return _queue.Min();
        }
    }
}
