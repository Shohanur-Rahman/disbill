using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentTicketBookingList
/// </summary>
public class StudentTicketBookingList
{
    public StudentTicketBookingList()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static IList<SingleTicketPurchase> _ticketEntityInSession;
    public static IList<SingleTicketPurchase> TicketEntityInSession
    {
        set
        {
            _ticketEntityInSession = value;
        }
        get
        {
            try
            {
                if (_ticketEntityInSession != null)
                    return _ticketEntityInSession;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }


    private static CustomerInformationInSession _customerEntityInSession;
    public static CustomerInformationInSession CustomerEntityInSession
    {
        set
        {
            _customerEntityInSession = value;
        }
        get
        {
            try
            {
                if (_customerEntityInSession != null)
                    return _customerEntityInSession;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}


public class CustomerInformationInSession {
    public string CustomerName { get; set; }
    public string TicketNumber { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string CustomerPayNumber { get; set; }
    public string CustomerTrNumber { get; set; }
}

public class SingleTicketPurchase {
    public int fromId { get; set; }
    public int toId { get; set; }
    public string startTime { get; set; }
    public string[] SeatNumberArray { get; set; }
    public int SeatCount { get; set; }
    public int TotalSeatPrice { get; set; }
    public string PricePerSeat { get; set; }
    public string BusNumber { get; set; }
    public string TourDate { get; set; }
    public string SeatNumbers { get; set; }
    public string TourRoot { get; set; }
    public string Destination { get; set; }
    public string TravelTime { get; set; }
    public string ReturnedDate { get; set; }
    public Boolean IsReturnDate { get; set; }
    
}