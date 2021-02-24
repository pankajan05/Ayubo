using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayubo
{
    class Calculation
    {
        //Store the package details
        IDictionary<string, double> package = new Dictionary<string, double>();

        //store the vehicle type
        IDictionary<string, double> vehicle_type = new Dictionary<string, double>();

        double weekly_rent = 2000.00;
        double daily_rent = 400.00;
        double daily_driver_cost = 1000.00;

        double extra_km_rate = 200.00;
        double waiting_charge_rate = 150.00;
        double overnight_charge_rate = 1500;


        //calculate rent
        public double rent_calculation(int vehicle_no, int rented_date, int return_date, bool with_driver) {
            double total_rent_value;
            int total_days;

            if (return_date - rented_date > 0) {
                total_days = return_date - rented_date;
            }
            else {
                total_days = (30 - rented_date) + return_date;
            }

            total_rent_value = (total_days / 7) * weekly_rent + (total_days % 7) * daily_rent;

            if (with_driver) {
                total_rent_value += daily_driver_cost * total_days;
            }

            return total_rent_value;

        }



        //We consider the vehicle didn't hire more than 1 month
        //calculate the day tour hire
        public Dictionary<string, double> day_tour_calculation(int vehicle_no, string package_type, double start_time, double end_time, int start_km_reading, int end_km_reading) { 

            double total_time = 0.0;
            double base_hire_charge = 0.00;
            double waiting_charge = 0.00;
            double extra_km_charge = (end_km_reading - start_km_reading - package[package_type]) * extra_km_rate;

            if (end_time - start_time > 0) {
                total_time = end_time - start_time;
            } else {
                total_time = (start_time - 24) + end_time;
            }

            waiting_charge = total_time * waiting_charge_rate;
            base_hire_charge = package[package_type];

            Dictionary<string, double> result = new Dictionary<string, double>() {
                { "base_hire_charge",base_hire_charge },
                { "waiting_charge",waiting_charge },
                {"extra_km_charge",extra_km_charge }
            };
            return result;


        }


        //calculate long tour package
        //We consider the vehicle didn't hire more than 1 month
        public Dictionary<string, double> long_tour_calculation(int vehicle_no, string package_type, int start_date, int end_date, int start_km_reading, int end_km_reading) {
            int total_day = 0;
            double base_hire_charge = 0.00;
            double overnight_stay_charge = 0.00;
            double extra_km_charge = (end_km_reading - start_km_reading - package[package_type]) * extra_km_rate;

            if (end_date - start_date > 0)
            {
                total_day = end_date - start_date;
            }
            else
            {
                total_day = (start_date - 30) + end_date;
            }

            overnight_stay_charge = total_day * overnight_charge_rate;
            base_hire_charge = package[package_type];

            Dictionary<string, double> result = new Dictionary<string, double>() {
                { "base_hire_charge",base_hire_charge },
                { "overnight_stay_charge",overnight_stay_charge },
                {"extra_km_charge",extra_km_charge }
            };
            return result;
        }

    }
}
