////using System.Collections;
////using System.Collections.Generic;
////using System.Linq;
////using Xunit;

////namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.Assertions
////{
////    public static class AssertionExtensions
////    {
////        public static void ShouldHaveSamePropertyValuesAs<T>(this T actual, T expected)
////        {
////            Assert.NotNull(actual);
////            Assert.NotNull(expected);
////            AssertObject(actual, expected);
////        }

////        private static void AssertObject(object actual, object expected)
////        {
////            Assert.False(actual == null ^ expected == null);

////            if (actual == null)
////            {
////                return;
////            }

////            var propInfos = actual.GetType().GetProperties();
////            foreach (var propInfo in propInfos)
////            {
////                var actualValue = propInfo.GetValue(actual);
////                var expectedValue = propInfo.GetValue(expected);
////                AssertProperty(actualValue, expectedValue);
////            }
////        }   

////        private static void AssertProperty(object actual, object expected)
////        {
////            var actualType = actual.GetType();
////            var expectedType = expected.GetType();

////            // We treat all enumerables as same
////            if (actual is IEnumerable actList && actualType != typeof(string))
////            {
////                var expectedList = expected as IEnumerable;
////                Assert.NotNull(expectedList);

////                var castedActualList = actList.Cast<object>().ToList();
////                var castedExpectedList = expectedList.Cast<object>().ToList();

////                for (var i = 0; i <= castedActualList.Count; i++)
////                {
////                    var actualEntry = castedActualList.ElementAt(i);
////                    var expectedEntry = castedExpectedList.ElementAt(i);

////                    AssertProperty(actualEntry, expectedEntry);
////                }

////                return;
////            }

////            if (actualType != expectedType)
////            {
////                Assert.True(false, $"Actual type {actualType.Name} not matching with expected type {expectedType.Name}");
////            }

////            if (actualType.IsPrimitive || actualType == typeof(string))
////            {
////                Assert.Equal(actual, expected);
////                return;
////            }

////            // If we're here, it's just a non-list object
////            AssertObject(actual, expected);
////        }
////    }
////}

