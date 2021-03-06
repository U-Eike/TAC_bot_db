import sys
import io
import string
import os
import json
import re
import math
import requests
import re
import jellyfish

re_job = re.compile(r'^([^_]*)_([^_]*)_(.*)$')
ReBr=re.compile(r'(\s+)?(\n)?(\[|\『|\().*')
u_m_names={}
birth={
    "その他"        : "Foreign World",
    "エンヴィリア"     : "Envylia",
    "スロウスシュタイン" : "Slothstein",
    "ルストブルグ"     : "Lustburg",
    "サガ地方"      : "Saga Region",
    "ラーストリス"     : "Wratharis",
    "ワダツミ"        : "Wadatsumi",
    "砂漠地帯"      : "Desert Zone",
    "ノーザンブライド"  : "Northern Pride",
    "グリードダイク"    : "Greed Dike",
    "グラトニー＝フォス" : "Gluttony Foss"
}  

def similarity(ori,inp):
    return (jellyfish.jaro_winkler(inp,ori))

def cPath():
    return os.path.dirname(os.path.realpath(__file__))

def loadFiles(files):
    ret=[]
    dir=os.path.dirname(os.path.realpath(__file__))+'\\res'
    
    for file in files:
        path = dir+'\\' + file
        if not os.path.exists(path):
            print(path, " was not found.")
            continue
        
        print (file)
        try:
            with open(path, "rt",encoding='utf8') as f:
                ret.append(json.loads(f.read()))  
        except ValueError:
            with open(path, "rt",encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read())) 
            
    
    return ret

def loadOut(files):
    ret=[]
    dir=os.path.dirname(os.path.realpath(__file__))+'\\out'
    
    for file in files:
        path = dir+'\\' + file
        if not os.path.exists(path):
            print(path, " was not found.")
            continue
        
        print (file)
        try:
            with open(path, "rt",encoding='utf8') as f:
                ret.append(json.loads(f.read()))  
        except ValueError:
            with open(path, "rt",encoding='utf-8-sig') as f:
                ret.append(json.loads(f.read())) 
            
    
    return ret
    
def saveAsJSON(name, var):
    os.makedirs(name[:name.rindex("\\")], exist_ok=True)
    with open(name, "wb") as f:
      f.write(json.dumps(var, indent=4, ensure_ascii=False).encode('utf8'))
    
    #try:
    #    with codecs.open(name, 'w', encoding='utf-8') as f:
    #        json.dump(var, f, indent=4, ensure_ascii=False)
    #except ValueError:
    #    print('Codecs Error, Retry')
    #    saveAsJSON(name,var)
    return 1    
    
def Translation():
    [loc, trans]=loadFiles(['LocalizedMasterParam.json','TranslationsS.json'])
    copy=['']
    for i in trans:
        if i in loc:
            loc[i].update(trans[i])
        else:
            loc[i]=trans[i]
            loc[i]['NAME']=trans[i]['name']
        
        if len(loc[i]['long des'])==0:
            loc[i]['long des']=loc[i]['short des']

    return loc

def convertMaster(master):
    ma={}
    for main in master:
        for sub in master[main]:
            try:
                ma[sub['iname']]=sub
            except:
                continue
    return ma

def element(type):
    if type == 1:
        return 'fire'
    if type in [2,10]:
        return 'water'
    if type in [3,100]:
        return 'wind'
    if type in [4,1000]:
        return 'thunder'
    if type in [5,10000]:
        return 'light'
    if type in [6,100000]:
        return 'dark'
    if type in [0,111111]:
        return 'all'
    return 'error'


def buff(buff,lv, mlv):
    global ParamTypes
    if len(ParamTypes)==0:
        [ParamTypes]=loadFiles(['ParamTypes.json'])

    text=""
    
    if 'elem' in buff:
        text+=element(buff['elem']).title()+': '
    if 'birth' in buff:
        text+=(birth[buff['birth']]).title()+': '

    allElem={'mod':"",'index':[]}
    allAtk={'mod':"",'index':[]}

    used=['type','vmax','vini']
    mods=[]
    for i in range(1,9):
        for u in used:
            if (u+'0'+str(i)) in buff:
                buff[u+str(i)]=buff[u+'0'+str(i)]

        if ('type'+str(i) in buff) and ('vmax'+str(i) in buff):
            btyp  = buff['type'+str(i)]
            bmin  = buff['vini'+str(i)]
            bmax  = buff['vmax'+str(i)]
            bmod  = ' ' if ('calc'+str(i) not in buff or buff['calc'+str(i)] == 0) else '% '

            try:
                number = math.floor(bmin + ((bmax - bmin) * (lv-1) / (mlv-1)))
                number = '+'+str(number) if  number>= 0 else str(number) #add + if pos
                mods.append(number + bmod + ParamTypes[str(btyp)])

                if 84 < btyp and btyp < 92:
                    allAtk['index'].append(i-1)
                    allAtk['mod']= number + bmod
                if 48 < btyp and btyp < 55:
                    allElem['index'].append(i-1)
                    allElem['mod']= number + bmod
            except:
                print(buff)
                print(btyp)
                print(bmin)
                print(bmax)
                print(bmod)

        else:
            break

    #reduce clutter | all elements & all dmg shortening
    #+25 Slash ATK & +25 Pierce ATK & +25 Strike ATK & +25 Missile ATK & +25 MATK & +25 JUMP ATK
    if len(allAtk['index'])==6:
        mods = [i for j, i in enumerate(mods) if j not in allAtk['index']]
        mods.append(allAtk['mod'] + 'Damage')
    if len(allElem['index'])==6:
        mods = [i for j, i in enumerate(mods) if j not in allElem['index']]
        mods.append(allElem['mod'] + 'Elemental Res')
        
                
    

    text= text + ' & '.join(mods)
    return text

def condition(cond, lv,  mlv):
    condType={
        0:'Poison',
        1:'Paralysed',
        2:'Stun',
        3:'Sleep',
        4:'Charm',
        5:'Stone',
        6:'Blindness',
        7:'Disable Skill',
        8:'Disable Move',
        9:'Disable Attack',
        10:'Zombie',
        11:'Death Sentence',
        12:'Berserk',
        13:'Disable Knockback',
        14:'Disable Buff',
        15:'Disable Debuff',
        16:'Stop',
        17:'Quicken',
        18:'Slow',
        19:'Auto Heal',
        20:'Donsoku',
        21:'Rage',
        22:'Good Sleep',
        23:'Auto Jewel',
        24:'Disable Heal',
        }
    if 'rini' in cond and cond['rini']<999:
        rate = math.floor(cond['rini'] + ((cond['rmax']-cond['rini']) * (lv-1) / (mlv-1)))
        return(condType[cond['conds'][0]] + ' ['+str(rate)+'%]')
    else:
        return(condType[cond['conds'][0]])

def rarity(base,max):
    text=""
    for i in range(0,base+1):
        text += "★"
    for i in range (base, max):
        text += "☆"
  
    return text

def GSSUpload(obj,sheet,id='1wze5mJCLeTZtBJiCrlOL4UMqUqc9mdBO_3siL2JtI3w'):
    url='https://script.google.com/macros/s/AKfycbzMqkHxhLsiMWqtFDmMHzqgT4a1R8yhBAxHN6YRkeN1lotYmsfg/exec'
    payload = json.dumps({
        "id": id,
        "sheet": sheet,
        "obj": obj
    })    

    response = requests.post(url, data=payload)

    #print(response.text) #TEXT/HTMLs
    res = json.loads(response.text)
    if  res['message']=='success':
        print('upload successfull')
    else:
        print(res)
        print(response.status_code, response.reason) #HTTPs

def dmg_formula(weaponParam):
    WeaponFormulaTypes={
        0   :"None",
        1   :"Atk",
        2   :"Mag",
        3   :"AtkSpd",
        4   :"MagSpd",
        5   :"AtkDex",
        6   :"MagDex",
        7   :"AtkLuk",
        8   :"MagLuk",
        9   :"AtkMag",
        10  :"SpAtk",
        11  :"SpMag",
        12  :"AtkSpdDex",
        13  :"MagSpdDex",
        14  :"AtkDexLuk",
        15  :"MagDexLuk",
        20  :"Def",
        21  :"Mnd",   
        }

    def modifier(num):
        return str(int(num*100))+"%"


    Formulas={
        "Atk" :    modifier(weaponParam['atk']/10) + " PATK",
        "Def" :    modifier(weaponParam['atk']/10) + " PDEF",
        "Mag" :    modifier(weaponParam['atk']/10) + " MATK",
        "Mnd" :    modifier(weaponParam['atk']/10) + " MDEF",
        "AtkSpd" : modifier(weaponParam['atk']/15) + " (PATK + AGI)",
        "MagSpd" : modifier(weaponParam['atk']/15) + " (MATK + AGI)",
        "AtkDex" : modifier(weaponParam['atk']/20) + " (PATK + DEX)",
        "MagDex" : modifier(weaponParam['atk']/20) + " (MATK + DEX)",
        "AtkLuk" : modifier(weaponParam['atk']/20) + " (PATK + LUCK)",
        "MagLuk" : modifier(weaponParam['atk']/20) + " (MATK + LUCK)",
        "AtkMag" : modifier(weaponParam['atk']/20) + " (PATK + MATK)",
        "SpAtk"  : modifier( weaponParam['atk']/10)+ " PATK * random(150% to 250%)",
        "SpMag"  : modifier(weaponParam['atk']/10) + " MATK * (20% + MATK/(MATK + Target's MDEF))",
        "AtkSpdDex" : modifier(weaponParam['atk']/20) + " (PATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "MagSpdDex" : modifier(weaponParam['atk']/20) + " (MATK + AGI/2 + AGI*LV/100 + DEX/4)",
        "AtkDexLuk" : modifier(weaponParam['atk']/20) + " (PATK + DEX/2 + LUCK/2)",
        "MagDexLuk" : modifier(weaponParam['atk']/20) + " (MATK + DEX/2 + LUCK/2)",
        #default = statusParam1.atk;
        }


    return Formulas[WeaponFormulaTypes[weaponParam['formula']]]


def dmg_calc(weaponParam, attacker,statusParam1, statusParam2):
    #  int num1 = 1;
    #  StatusParam statusParam1 = attacker.CurrentStatus.param;
    #  StatusParam statusParam2 = defender == null ? (StatusParam) null : defender.CurrentStatus.param;
    #  int num2;
    WeaponFormulaTypes=[
    "None",
    "Atk",
    "Mag",
    "AtkSpd",
    "MagSpd",
    "AtkDex",
    "MagDex",
    "AtkLuk",
    "MagLuk",
    "AtkMag",
    "SpAtk",
    "SpMag",
    "AtkSpdDex",
    "MagSpdDex",
    "AtkDexLuk",
    "MagDexLuk",
    ]

    Formulas={#WeaponFormulaTypes[master[weapon]['formula']]):
        "Atk"       : weaponParam.atk * (100 * int(statusParam1.atk) / 10) / 100,
        "Mag"       : weaponParam.atk * (100 * int(statusParam1.mag) / 10) / 100,
        "AtkSpd"    : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.spd))) / 15 / 100,
        "MagSpd"    : weaponParam.atk * (100 * (int(statusParam1.mag) + int(statusParam1.spd))) / 15 / 100,
        "AtkDex"    : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.dex))) / 20 / 100,
        "MagDex"    : weaponParam.atk * (100 * (int(statusParam1.mag) + int(statusParam1.dex))) / 20 / 100,
        "AtkLuk"    : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.luk))) / 20 / 100,
        "MagLuk"    : weaponParam.atk * (100 * (int(statusParam1.mag) + int(statusParam1.luk))) / 20 / 100,
        "AtkMag"    : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.mag))) / 20 / 100,
        #"SpAtk": {
        #    if (int(statusParam1.atk) > 0)
        #      num1 += int(((long) this.GetRandom() % (long) int(statusParam1.atk)))
        #    num2 = int(weaponParam.atk) * (100 * int(statusParam1.atk) / 10) * (50 + 100 * (1+()) / int(statusParam1.atk)) / 10000
        #},
        "SpMag": int(weaponParam.atk) * (100 * int(statusParam1.mag) / 10) * (20 + 100 / (int(statusParam1.mag) + num3) * int(statusParam1.mag)) / 10000,
        "AtkSpdDex" : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.spd) / 2 + int(statusParam1.spd) * attacker.Lv / 100 + int(statusParam1.dex / 4))) / 20 / 100,
        "MagSpdDex" : weaponParam.atk * (100 * (int(statusParam1.mag) + int(statusParam1.spd) / 2 + int(statusParam1.spd) * attacker.Lv / 100 + int(statusParam1.dex / 4))) / 20 / 100,
        "AtkDexLuk" : weaponParam.atk * (100 * (int(statusParam1.atk) + int(statusParam1.dex) / 2 + int(statusParam1.luk) / 2)) / 20 / 100,
        "MagDexLuk" : weaponParam.atk * (100 * (int(statusParam1.mag) + int(statusParam1.dex) / 2 + int(statusParam1.luk) / 2)) / 20 / 100,
        #default : statusParam1.atk;
        }

def name_collab(iname,loc):
    global u_m_names

    iname = iname[6:]
    
    #collab 
    collabs={
        'FATE': ['Fate' ,'Fate/Stay Night [UBW]'],
        'FA'  : ['FA'   ,'Fullmetal Alchemist'],
        'RH'  : ['RH'   ,'Radiant Historia'],
        'POK' : ['POTK' ,'Phantom Of The Kill'],
        'S'   : ['SN'   ,'Shinobi Nightmare'],
        'FF15': ['FF'   ,'Final Fantasy 15'],
        'DIS' : ['DIS'  ,'Disgea'],
        'SEK' : ['EO'   ,'Etrian Odyssey'],
        'SQ'  : ['EMD'  ,'Etrian Mystery Dungeon'],
        'APR' : [''     ,'April Fool'],
        'CRY' : ['CR'   ,'Crystal Re:Union']
        }
        
    collab=""
    collab_short=''    
    for c in collabs:
        if c+'_' == iname[:len(c)+1]:
            collab = collabs[c][1]
            collab_short = collabs[c][0]
            iname= iname[len(c)+1:]
            break
            
    #unknown name
    try:
        name = loc['UN_V2_'+iname]['NAME']
    except:
        #print(iname)
        name = iname.replace('_',' ')
        prefix={
            'BLK' : 'Black Killer',
            'L'   : 'Little',
            }
        
        for p in prefix:
            if  p+' ' == name[:len(p)+1]:
                name = prefix[p] + ' ' + name[len(p)+1:]
        
        name=name.title()
        u_m_names['UN_V2_'+iname]={'generic':name, 'NAME':""}
        
    return [name,collab,collab_short]
 

def FanTranslatedNames(wyte,master,loc):
    wunit={}
    for unit in wyte['Units']:
        wunit[unit['Name JPN']]=unit


    found={}
    none={}
    for unit in master['Unit']:
        if unit['ai'] == "AI_PLAYER":
            [name,collab,collab_short]=name_collab(unit['iname'],loc)
            try:
                found[unit['iname']]={
                    'iname':unit['iname'], 
                    'official': loc[unit['iname']]['NAME'] if unit['iname'] in loc else name,
                    'generated': name,
                    'inofficial': ReBr.sub('',wunit[unit['name']]['Name']).rstrip(' '),
                    'inofficial2': wunit[unit['name']]['Name'],
                    'collab': collab,
                    'collab_short':collab_short,
                    'japanese': unit['name']
                    }
                del wunit[unit['name']]
            except:
                none[unit['iname']]={
                    'iname':unit['iname'], 
                    'official': loc[unit['iname']]['NAME'] if unit['iname'] in loc else name,
                    'generated': name,
                    'inofficial': "",
                    'collab': collab,
                    'collab_short':collab_short,
                    'japanese': unit['name']
                    }
        else:
            break

    delete=[]

    for unit in none:
        try:
            for left in wunit:
                if  (similarity(none[unit]['official'],ReBr.sub('',wunit[left]['Name']).title())>=0.8 or
                    similarity(none[unit]['official'],wunit[left]['Romanji'].title())>=0.8 or
                    similarity(none[unit]['japanese'],left)>=0.8):

                    found[none[unit]['iname']]=none[unit]
                    found[none[unit]['iname']].update({
                        'inofficial': ReBr.sub('',wunit[left]['Name']).rstrip(' '),
                        'inofficial2': wunit[left]['Name'],
                        'japanese2': wunit[left]['Name JPN']
                    })
                    del wunit[left]
                    delete.append(unit)
                    break
                
        except RuntimeError:
            pass

    for i in delete:
        try:
            del none[i]
        except: 
            print(i)
    found.update(none)
    return(found)
 ## other stuff
[ParamTypes]=loadFiles(['ParamTypes.json'])