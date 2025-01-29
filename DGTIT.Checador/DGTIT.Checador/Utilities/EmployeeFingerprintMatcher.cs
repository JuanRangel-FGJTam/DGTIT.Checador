using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPFP;
using DGTIT.Checador.Core.Entities;
using DGTIT.Checador.Models;

namespace DGTIT.Checador.Utilities
{
    internal class EmployeeFingerprintMatcher
    {
        private readonly IEnumerable<int> areasAvailables = Array.Empty<int>();
        public EmployeeFingerprintMatcher(IEnumerable<int> areasAvailables )
        {
            this.areasAvailables = areasAvailables;
        }

        public OperationMatchingResult<Employee> GetMarchingEmployee(IEnumerable<Employee> employees, DPFP.FeatureSet features)
        {
            // * prepare for validate each employees fingerprints
            var verificator =  new DPFP.Verification.Verification();
            var result = new DPFP.Verification.Verification.Result();
            var template = new DPFP.Template();

            // * compare each employee
            foreach (var emplooyee in employees)
            {
                if (emplooyee.Fingerprint == null)
                {
                    continue;
                }
                if(!emplooyee.Fingerprint.Any())
                {
                    continue;
                }

                // * validate the fingerprint of each employee
                using (Stream ms = new MemoryStream(emplooyee.Fingerprint))
                {
                    template = new DPFP.Template(ms);
                }

                verificator.Verify(features, template, ref result);
                if (!result.Verified)
                {
                    continue;
                }

                // MakeReport($"Empledo No {emp.employee_number} encontrado", EventLevel.Informational);

                // * validete if the employee is active
                if (!emplooyee.Active)
                {
                    return OperationMatchingResult<Employee>.Failure(MatchingStatus.INACTIVE, emplooyee);
                }

                // * validete if the employee direction has assigned the area
                if (emplooyee.GeneralDirectionId <= 0)
                {
                    return OperationMatchingResult<Employee>.Failure(MatchingStatus.BAD_AREA, emplooyee);
                }
                if (!this.areasAvailables.Contains( emplooyee.GeneralDirectionId))
                {
                    return OperationMatchingResult<Employee>.Failure(MatchingStatus.BAD_AREA, emplooyee);
                }

                return OperationMatchingResult<Employee>.Success(emplooyee);
            }

            return OperationMatchingResult<Employee>.Failure(MatchingStatus.NOT_FOUND);
        }

    }
}
