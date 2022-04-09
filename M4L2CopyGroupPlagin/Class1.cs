using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4L2CopyGroupPlagin
{
    [TransactionAttribute(TransactionMode.Manual)]//
    public class CopyGroup : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;//получение ссылки на Юай документ
            Document doc = uiDoc.Document;//получение ссылки на экземпляр класса документ, соссылкой на бд открытого документ


            //Выбор группы для копирования

            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберете группу объектов");//получение ссылки на объект
            Element element = doc.GetElement(reference); //получение самого объекта
            Group group = element as Group; //преобразование элемента в группу

            //Выбор точек вставки

            XYZ point = uiDoc.Selection.PickPoint("Выберете точку");

            //вставка группы в точку

            Transaction transaction = new Transaction(doc);
            transaction.Start("Копирование группы");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
