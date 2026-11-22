using AutoMapper;
using System;
using TaxCalculation.Application.DTOs;
using TaxCalculation.Application.Interface;
using TaxCalculation.Domain.InterfaceInfrastructure;

namespace TaxCalculation.Application.Services
{
    public class GetEmployeeService : IGetEmployeeService
    {
        private readonly IGetEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeService(IGetEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> employeesDetailsAsync()
        {
            try
            {
                var dto = await _employeeRepository.employeesDetailsAsync();
                return _mapper.Map<List<EmployeeDto>>(dto);

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
