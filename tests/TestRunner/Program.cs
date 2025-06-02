using System;
using SampleEcomStoreApi.Tests;
using SampleEcomStoreApi.IntegrationTests;

namespace TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Running Sample Ecommerce Store API Tests ===");
            Console.WriteLine();
            
            // Run Unit Tests
            Console.WriteLine("1. Running Unit Tests...");
            Console.WriteLine("========================");
            try
            {
                ProductManagerTests.RunAllTests();
                Console.WriteLine("✓ Unit tests completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Unit tests failed: {ex.Message}");
            }
            
            Console.WriteLine();
            
            // Run Integration Tests  
            Console.WriteLine("2. Running Integration Tests...");
            Console.WriteLine("===============================");
            try
            {
                ProductServiceIntegrationTests.RunAllTests();
                Console.WriteLine("✓ Integration tests completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Integration tests failed: {ex.Message}");
            }
            
            Console.WriteLine();
            Console.WriteLine("=== Test Run Complete ===");
            // Remove ReadKey to avoid console input issues
        }
    }
}
