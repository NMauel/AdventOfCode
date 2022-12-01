using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day19 : IPuzzleDay<int>
    {
        private readonly IEnumerable<string> input = InputReader.ReadLines("Day19.txt");

        public int CalculateAnswerPuzzle1()
        {
            //Define beaconsmap (oerientation seen from first scanner)
            var scanners = ParseToScanners(input).ToArray();
            var fullBeaconsMap = MapBeaconsFromScanners(scanners);

            /*for (int a = 0; a < scanners.Length - 1; a++)
            {
                for (int b = a + 1; b < scanners.Length; b++)
                {
                    var distanceMatches = scanners[b].Distances.Where(bd => scanners[a].Distances.Any(ad => ad.Distance.Equals(bd.Distance)));
                    var matchingBeacons = distanceMatches.Select(x => x.Beacon1).Union(distanceMatches.Select(x => x.Beacon2));

                    if (matchingBeacons.Count() >= 12)
                    {
                        Console.WriteLine($"Scanners {scanners[a].ScannerID} & {scanners[b].ScannerID} SHARE {matchingBeacons.Count()} BEACONS!");
                        
                        DetermineScannerPosition(scanners[b], scanners[a]);
                        Console.WriteLine($"Position of scanner is: {scanners[b].Position}");

                        beaconCount += matchingBeacons.Count();

                        //Convert all beacons to coordinates from scanner 0.
                        //allBeacons.AddRange(TransformBeaconCoordinates(scanners[b]));
                    }
                }
            }*/

            return fullBeaconsMap.Count();
        }

        private IEnumerable<Beacon> MapBeaconsFromScanners(Scanner[] scanners)
        {
            for (int a = 0; a < scanners.Length - 1; a++)
            {
                var refScanner = scanners[a];
                for (int b = a + 1; b < scanners.Length; b++)
                {
                    var scanner = scanners[b];
                    if (scanner.Position != null)
                        continue;

                    if(CompareBeacons(refScanner, scanner))
                    {
                        //OrientateBeaconsAndScanner();
                    }
                }
            }

           yield break;
        }

        private bool CompareBeacons(Scanner refScanner, Scanner scanner)
        {
            var distanceMatches = scanner.Distances.Where(sd => refScanner.Distances.Any(rd => rd.Distance.Equals(sd.Distance)));
            var matchingBeacons = distanceMatches.Select(x => x.Beacon1).Union(distanceMatches.Select(x => x.Beacon2));
            if(matchingBeacons.Count() >= 12)
            {
                //We have a match!
                return true;
            }

            return false;
        }

        public int CalculateAnswerPuzzle2()
        {
            return 0;
        }

        private void DetermineScannerPosition(Scanner scanner, Scanner referenceScanner)
        {


            /*Coordinate? position = null;
            //for (int i = 0; i < 24; i++)
            //{
                var count = scanner.Distances.Count(b => referenceScanner.Distances.Any(rb => rb.Equals(b)));
                //var count = scanner.BeaconCoordinates.Count(b => referenceScanner.BeaconCoordinates.Any(rb => rb.Equals(b)));
                if (count >= 12)
                {
                    Console.WriteLine($"Scanner {scanner.ScannerID} has {count} distance matches.");   
                    //Found match... Todo: determine position...
                    position = new Coordinate(0, 0, 0);
                    //break;
                }
                //scanner.SwitchOrientation();
                Console.WriteLine(scanner.Beacons.First());
            //}
            if (!position.HasValue)
                throw new Exception($"Failed to determine position for scanner {scanner.ScannerID} (referenced to scanner {referenceScanner.ScannerID}).");
            scanner.Position = position.Value;
            */
        }

        //private IEnumerable<Beacon> TransformBeaconCoordinates(Scanner scanner)
        //{
        //    throw new NotImplementedException();
        //}

        private class Scanner
        {
            private readonly List<Beacon> beacons;
            private readonly List<BeaconDistance> distances;

            public int ScannerID { get; }
            public Coordinate? Position { get; set; }

            public IEnumerable<Beacon> Beacons => beacons;
            public IEnumerable<BeaconDistance> Distances => distances;

            public Scanner(int scannerID, Coordinate[] beaconCoordinates)
            {
                ScannerID = scannerID;
                beacons = new(beaconCoordinates.Select(b => new Beacon(b)));
                distances = new List<BeaconDistance>();

                for (int a = 0; a < beacons.Count - 1; a++)
                    for (int b = a + 1; b < beacons.Count; b++)
                        distances.Add(new BeaconDistance(beacons[a], beacons[b]));
            }

            public override string ToString()
            {
                var result = $"--- scanner {ScannerID} ---\r\n";

                foreach (var beacon in Beacons)
                    result += $"{beacon}\r\n";

                result += $"\r\n---------------------\r\n";

                foreach (var distance in Distances)
                    result += $"{distance}\r\n";

                return result;
            }
        }

        private class Beacon
        {
            public Guid ID { get; } = Guid.NewGuid();
            public Coordinate Position { get; }

            public Beacon(Coordinate position)
            {
                Position = position;
            }

            public override string ToString() => $"{ID} - {Position}";
        }

        private class BeaconDistance
        {
            public Beacon Beacon1 { get; }
            public Beacon Beacon2 { get; }

            public double Distance { get; }

            public BeaconDistance(Beacon beacon1, Beacon beacon2)
            {
                Beacon1 = beacon1;
                Beacon2 = beacon2;
                Distance = Math.Pow(Math.Pow(beacon1.Position.X - beacon2.Position.X, 2) +
                                    Math.Pow(beacon1.Position.Y - beacon2.Position.Y, 2) +
                                    Math.Pow(beacon1.Position.Z - beacon2.Position.Z, 2), 0.5);
            }

            public override bool Equals(object obj)
            {
                if (obj is BeaconDistance other)
                    return Distance.Equals(other.Distance);
                return false;
            }

            public override int GetHashCode() => (int)Distance * 1000;

            public override string ToString() => $"[{Beacon1.ID} - {Beacon2.ID}]\t {Distance}";
        }

        private struct Coordinate
        {
            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public Coordinate(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public override bool Equals(object obj)
            {
                if(obj is Coordinate other)
                    return X == other.X && Y == other.Y && Z == other.Z;
                return false;
            }

            public override int GetHashCode() => 0;

            public override string ToString() => $"{X},{Y},{Z}";
        }

        private static IEnumerable<Scanner> ParseToScanners(IEnumerable<string> input)
        {
            int currentScannerID = -1;
            var beaconCoordinates = new List<Coordinate>();

            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                if (line.StartsWith("---"))
                {
                    if (currentScannerID > -1)
                    {
                        yield return new Scanner(currentScannerID, beaconCoordinates.ToArray());
                        beaconCoordinates.Clear();
                    }
                    currentScannerID = int.Parse(line.Trim('-', ' ').Split(' ')[1]);
                }
                else
                {
                    var coords = line.Split(',').Select(int.Parse).ToArray();
                    beaconCoordinates.Add(new Coordinate(coords[0], coords[1], coords[2]));
                }
            }
            yield return new Scanner(currentScannerID, beaconCoordinates.ToArray());
        }
    }
}