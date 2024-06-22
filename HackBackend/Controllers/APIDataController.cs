using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DistinctLocationsApi.Models;

namespace APIDataController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIDataController : ControllerBase
    {
        private const string FilePath = @"C:\Users\lenovo\Desktop\Hack\allData.json";

        [HttpGet("DistinctLocation")]
        public IActionResult GetDistinctLocations()
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var distinctLocations = roots
                                    .SelectMany(root => root.Items)
                                    .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
                                    && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
                                    && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase))
                                    .Select(item => item.location)
                                    .Distinct()
                                    .ToList();

            //var distinctLocations = roots
            //    .SelectMany(root => root.Items)
            //    .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
            //    && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
            //    && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase))
            //    .GroupBy(item => item.location)
            //    .Select(group => group.First())
            //    .ToList();


            return Ok(distinctLocations);
        }

        [HttpGet("VirtualMachinesWindowsSize")]
        public IActionResult GetWindowsVirtualMachinesSize(string location)
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var virtualMachines = roots
                                .SelectMany(root => root.Items)
                                .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
                                               && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
                                               && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
                                               && item.productName.Contains("Windows", StringComparison.OrdinalIgnoreCase)
                                               && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
                                               && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase))
                                .Select(item => item.armSkuName)
                                .Distinct()
                                .ToList();

            //var virtualMachines1 = roots
            //                    .SelectMany(root => root.Items)
            //                    .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
            //                                   && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
            //                                   && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
            //                                   && item.productName.Contains("Windows", StringComparison.OrdinalIgnoreCase)
            //                                   && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
            //                                   && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase))
            //                    .GroupBy(item => item.armSkuName)
            //                    .Select(group => group.First())
            //                    .ToList();
            return Ok(virtualMachines);
        }


        [HttpGet("VirtualMachinesLinuxSize")]
        public IActionResult GetLinuxVirtualMachinesSize(string location)
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var virtualMachines = roots
                                .SelectMany(root => root.Items)
                                .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
                                                && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
                                                && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
                                                && item.productName.Contains("Linux", StringComparison.OrdinalIgnoreCase)
                                                && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
                                                && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase))
                                .GroupBy(item => item.armSkuName)
                                .Select(group => group.Key) // Select the productName directly from the group
                                .ToList();

            //var virtualMachines1 = roots
            //        .SelectMany(root => root.Items)
            //        .Where(item => item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
            //                       && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
            //                       && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
            //                       && item.productName.Contains("Linux", StringComparison.OrdinalIgnoreCase)
            //                       && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
            //                       && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase))
            //        .GroupBy(item => item.armSkuName)
            //        .Select(group => group.First())
            //        .ToList();

            return Ok(virtualMachines);
        }


        [HttpGet("GetStorageType")]
        public IActionResult GetStorageType(string location)
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var storage = roots
                            .SelectMany(root => root.Items)
                            .Where(item => item.serviceFamily.Equals("Storage", StringComparison.OrdinalIgnoreCase)
                                && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
                                && item.productName.Contains("Managed Disks", StringComparison.OrdinalIgnoreCase))
                            .Select(item => item.armSkuName)
                            .Distinct()
                            .ToList();

            //var storage1 = roots
            //                .SelectMany(root => root.Items)
            //                .Where(item => item.serviceFamily.Equals("Storage", StringComparison.OrdinalIgnoreCase)
            //                    && item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
            //                    && item.productName.Contains("Managed Disks", StringComparison.OrdinalIgnoreCase))
            //                .GroupBy(group => group.armSkuName)
            //                .Select(group => group.First())
            //                .ToList();

            //foreach (var item in storage)
            //{
            //    if (item.armSkuName == null || item.armSkuName == "")
            //    {
            //        item.armSkuName = item.productName;
            //    }
            //}
            return Ok(storage);
        }


        //[HttpGet("GetStorageSize")]
        //public IActionResult GetStorageSize(string location)
        //{
        //    if (!System.IO.File.Exists(FilePath))
        //    {
        //        return NotFound("File not found.");
        //    }

        //    string json = System.IO.File.ReadAllText(FilePath);
        //    var roots = JsonConvert.DeserializeObject<List<Root>>(json);

        //    var storage = roots
        //                    .SelectMany(root => root.Items)
        //                    .Where(item => item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
        //                        && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
        //                        && item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
        //                        && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
        //                        && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase))
        //                    .Select(item => item.armSkuName)
        //                    .Distinct()
        //                    .ToList();
        //    return Ok(storage);
        //}

        [HttpGet("GetVMPrice")]
        public IActionResult GetVMPrice(string location,  string vmSize)
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var prices = roots
                .SelectMany(root => root.Items)
                .Where(item => item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
                && item.serviceFamily.Equals("Compute", StringComparison.OrdinalIgnoreCase)
                && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
                && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase)
                && item.serviceName.Equals("Virtual Machines", StringComparison.OrdinalIgnoreCase)
                && item.armSkuName.Equals(vmSize, StringComparison.OrdinalIgnoreCase))
                .ToList();

            double minPrice = 10000000;
            foreach (var item in prices)
            {
                if(minPrice > item.unitPrice)
                {
                    minPrice = item.unitPrice;
                }
            }

            return Ok(minPrice);
        }


        [HttpGet("GetStoragePrice")]
        public IActionResult GetStoragePrice(string location, string storage) 
        {
            if (!System.IO.File.Exists(FilePath))
            {
                return NotFound("File not found.");
            }

            string json = System.IO.File.ReadAllText(FilePath);
            var roots = JsonConvert.DeserializeObject<List<Root>>(json);

            var prices = roots
                .SelectMany(root => root.Items)
                .Where(item => item.location.Equals(location, StringComparison.OrdinalIgnoreCase)
                && item.serviceFamily.Equals("Storage", StringComparison.OrdinalIgnoreCase)
                && item.type.Equals("Consumption", StringComparison.OrdinalIgnoreCase)
                && !item.skuName.Contains("Spot", StringComparison.OrdinalIgnoreCase)
                && item.serviceName.Equals("Storage", StringComparison.OrdinalIgnoreCase)
                && item.armSkuName.Equals(storage, StringComparison.OrdinalIgnoreCase))
                .ToList();


            double price = 100000000;
            foreach (var item in prices)
            {
                if(price > item.unitPrice)
                {
                    price = item.unitPrice / 30;
                }         
            }
            Console.WriteLine(price);
            return Ok(price);
        }

    }
}
