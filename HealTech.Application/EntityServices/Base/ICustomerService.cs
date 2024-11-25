﻿using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface ICustomerService
{
    Task Register(string firstname, string surname, string username, string passwordHash, string taxNumber, string email, string phone, string address);
    Task Update(Guid id, string firstname, string surname, string username, string passwordHash, string taxNumber, string email, string phone, string address, bool isActive = true);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(Guid id);
}