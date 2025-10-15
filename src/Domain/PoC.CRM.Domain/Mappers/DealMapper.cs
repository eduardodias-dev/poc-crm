using System;
using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Mappers;

public class DealMapper
{
    public static Deal MapFromDB(dynamic? data){
        if (data == null) return null!;

        var stage = (DealStage)data.stage;
        return new Deal(data.id, data.company_name, stage, data.title, data.amount, 
        new DealCode(data.code), data.closing_date, data.lost_reason);
    }

    public static Deal MapToDB(dynamic data){
        if (data == null) return null!;

        var stage = data.stage;
        return new Deal(data.id, data.company_name, (DealStage)stage, data.title, data.amount, 
        new DealCode(data.code), data.closing_date, data.lost_reason);
    }
}
