﻿using System.Security.AccessControl;

namespace WebCommerce.Entities.Infos;

public record ProductInfo(int Id, string Name, decimal UnitPrice, string CategoryName, string ImageUrl);