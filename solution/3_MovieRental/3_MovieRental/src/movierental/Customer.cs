﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace movierental
{
    public class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string getName()
        {
            return _name;
        }

        /// <summary>
        /// Het doel van deze oefening is de onderstaande code te refactoren. Refactoren is de interne code structuur verbeteren
        /// zonder daarbij de externe functionaliteit te veranderen.
        /// </summary>
        /// <returns></returns>
        public string statement() {
		    double totalAmount = 0;
		    int frequentRenterPoints = 0;
		    string result = "Rental Record for " + getName() + "\n";
		
		    foreach (Rental each in _rentals) {
			    double thisAmount = 0;
			
			    //determine amounts for each line
			    switch (each.getMovie().getPriceCode()) {
			    case Movie.REGULAR:
				    thisAmount += 2;
				    if (each.getDaysRented() > 2) 
					    thisAmount += (each.getDaysRented() - 2) * 1.5;
				    break;
			    case Movie.NEW_RELEASE:
				    thisAmount += each.getDaysRented() * 3;
				    break;
			    case Movie.CHILDRENS:
				    thisAmount += 1.5;
				    if (each.getDaysRented() > 3)
					    thisAmount += (each.getDaysRented() - 3) * 1.5;
				    break;
			    }
			
			    // add frequent renter points
			    frequentRenterPoints++;
			    // add bonus for a two day new release rental
			    if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) && each.getDaysRented() > 1) 
				    frequentRenterPoints++;
			
			    // show figures for this rental
                result += "\t" + each.getMovie().getTitle() + "\t" + thisAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
			    totalAmount += thisAmount;
		    }
		
		    // add footer lines
		    result += "Amount owed is " + totalAmount.ToString("0.0", CultureInfo.InvariantCulture) + "\n";
		    result += "You earned " + frequentRenterPoints + " frequent renter points";
		
		    return result;
	    }
    }
}
