using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class ProductManager : MonoBehaviour
{
    private string path = "//DESKTOP-1UKP42J/Arbetsprov/Produkter.txt";
    [SerializeField] GameObject ProductPrefab;
    [SerializeField] Transform ProductPanel;
    [SerializeField] List<GameObject> products = new List<GameObject>(); //A list of current products that exist in the source file
    

    //Gets the file that the product information is saved in
    string[] GetSourceFile()
    {
        return File.ReadAllLines(path);
    }
    
    //Opens the produts Tab and displays all the available products
    public void OpenProductsMenu()
    {
        DeleteInstancesOnPageClosed(); //Deletes any previous instances of product objucts to make sure there are no duplicates
        string[] lines = GetSourceFile();
        int i = 0;
        foreach (string line in lines)
        {
            string myLine = line;

            GameObject prodInstance = Instantiate(ProductPrefab, ProductPanel);
            products.Add(prodInstance);
            prodInstance.GetComponent<Product>().line = i;
            prodInstance.GetComponent<Product>().number.text = myLine.Substring(0, myLine.IndexOf(","));
            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);

            prodInstance.GetComponent<Product>().productName.text = myLine.Substring(0, myLine.IndexOf(","));
            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);


            prodInstance.GetComponent<Product>().price.text = myLine.Substring(0, myLine.IndexOf(";"));
            i++;
        }
        //StartCoroutine(UpdateLines());
    }

    //Refreshes the lines containing product information every second
    //IEnumerator UpdateLines()
    //{
        
    //    while(ProductPanel.gameObject.activeSelf)
    //    {
    //        string[] lines = GetSourceFile();
    //        int i = 0;
    //        foreach (string line in lines)
    //        {
    //            string myLine = line;

    //            products[i].GetComponent<Product>().number.text = myLine.Substring(0, myLine.IndexOf(","));
    //            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);

    //            products[i].GetComponent<Product>().productName.text = myLine.Substring(0, myLine.IndexOf(","));
    //            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);


    //            products[i].GetComponent<Product>().price.text = myLine.Substring(0, myLine.IndexOf(";"));
    //            i++;
    //            yield return new WaitForSeconds(1);
    //        }
    //    }
    //    yield return null;
        
    //}

    public void EditProduct(ProductEditor editor)
    {
        string[] lines = GetSourceFile();
        List<string> newLines = new List<string>();
        newLines = lines.ToList();
        int n = 0;

        foreach (string line in newLines)
        {
            if (!string.IsNullOrEmpty(editor.number.text) && line.Contains(editor.number.text))
            {
                string newName = products[n].GetComponent<Product>().productName.text; //By default, newName equals old name
                int newPrice = int.Parse(products[n].GetComponent<Product>().price.text);

                if (!string.IsNullOrEmpty(editor.productName.text))
                    newName = editor.productName.text;
                if (!string.IsNullOrEmpty(editor.price.text))
                    newPrice = int.Parse(editor.price.text);

                newLines.Remove(line);
                newLines.Insert(n, editor.number.text.ToUpper() + "," + newName + "," + newPrice + ";");
                File.WriteAllLines(path, newLines);
                break;
            }
            n++;
        }
    }

    //Deletes all object instances that were stored in the List 'products'
    public void DeleteInstancesOnPageClosed()
    {
        foreach(GameObject prod in products)
        {
            Destroy(prod);
        }
        products.Clear();
    }

    public void AddNewProduct(NewProduct prod)
    {
        string[] lines = GetSourceFile();
        List<string> newLines = new List<string>();
        newLines = lines.ToList();
        newLines.Add(prod.number.ToUpper() + "," + prod.productName + "," + prod.price + ";");
        File.WriteAllLines(path, newLines);
    }

    public void DeleteProduct(Text productNumber)
    {
        string[] lines = GetSourceFile();
        List<string> newLines = new List<string>();
        newLines = lines.ToList();

        foreach (string line in newLines)
        {
            if (line.Contains(productNumber.text.ToUpper()) &&  !string.IsNullOrEmpty(productNumber.text))
            {
                newLines.Remove(line); 
                break;
            }
        }

        File.WriteAllLines(path, newLines);
    }
}
