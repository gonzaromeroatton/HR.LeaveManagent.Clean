using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorUI.Modules;

public sealed class LeaveTypeViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [Display(Name = "Default Number of Days")]
    public int DefaultDays { get; set; }
}
