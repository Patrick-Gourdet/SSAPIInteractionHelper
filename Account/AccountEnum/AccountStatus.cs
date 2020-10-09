using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSAPIInteractionHelper.Account.AccountEnum
{
    /// <summary>
    /// ONBOARDING The account is onboarding.
    /// SUBMISSION_FAILED The account application submission failed for some reason.
    /// SUBMITTED The account application has been submitted for review.
    /// ACCOUNT_UPDATED The account information is being updated.
    /// APPROVAL_PENDING The final account approval is pending.
    /// ACTIVE The account is active for trading.
    /// REJECTED The account application has been rejected
    /// </summary>
    /// <remarks>
    /// This is to query current account standing
    /// This emulates exact structure of the C#
    /// Alpaca API which would allow to create
    /// different query methods using said API
    /// </remarks>
    public enum AccountStatus
    {
        ONBOARDING,
        SUBMISSION_FAILED,
        SUBMITTED,
        ACCOUNT_UPDATED,
        APPROVAL_PENDING,
        ACTIVE,
        REJECTED
    }
}
