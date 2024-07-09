namespace BetPlacer.Punter.API.Utils.Normalization
{
    public class ValueNormalizer
    {
        private Queue<double?> _queue;
        private Queue<Tuple<int, double?>> _valuePerId;

        public ValueNormalizer()
        {
            _queue = new Queue<double?>(5);
            _valuePerId = new Queue<Tuple<int, double?>>(5);
        }

        public double?[] ToArray()
        {
            return _queue.ToArray();
        }

        public double Normalize(int matchCode, double? value, int index)
        {
            Enqueue(matchCode, value);

            if (index < 4 || value == null)
                return 0;

            double maxValue = GetMaxItem();
            double minValue = GetMinItem();

            double dividend = value.Value - minValue;
            double divider = maxValue - minValue;

            if (divider == 0)
                return 0;

            double result = dividend / divider;
            return result;
        }

        public void Enqueue(int matchCode, double? item)
        {
            if (_queue.Count >= 5)
            {
                _queue.Dequeue();
                _valuePerId.Dequeue();
            }

            _queue.Enqueue(item);
            _valuePerId.Enqueue(Tuple.Create(matchCode, item));
        }

        private double GetMaxItem()
        {
            return _queue.Max().Value;
        }

        private double GetMinItem()
        {
            return _queue.Min().Value;
        }
    }
}
