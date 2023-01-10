using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CalculatonDAL.Models
{
    [Table("calcution_item")]
    public class CalculationItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("calculation_id")]
        public int CalculationId { get; set; } = 0;
        [Column("number_one")]
        public int NumberOne { get; set; } = 0;
        [Column("number_two")]
        public int NumberTwo { get; set; } = 0;
        [Column("operation_code")]
        public string OperationCode { get; set; } = "ADD";
        [Column("result_value")]
        public int Result_Value { get; set; } = 0;
    }
}
